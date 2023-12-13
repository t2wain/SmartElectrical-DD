using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using SELDictionary.Model;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using static SELDictionary.ItemSQL;

namespace SELDictionary
{
    /// <summary>
    /// This is the main class that implements the logic
    /// to generate the SQL statements. First, query the
    /// DbContext for an Item, then use this class to
    /// generate these SQL objects.
    /// </summary>
    public class ItemSQL
    {
        private readonly IServiceProvider _provider;

        #region Classes

        /// <summary>
        /// Table / Column reference in a join
        /// </summary>
        private record TblCol
        {
            public string Table { get; set; } = "";
            public string AppName { get; set; } = "";
            public string? CorAttType { get; set; }
            public int? CorAttNo { get; set; }
            /// <summary>
            /// A zero (0) sequence value indicates table
            /// alias is not required in the join clause.
            /// </summary>
            public int Sequence { get; set; } = 0;
            public string Column { get; set; } = "";
            public string FromExpr =>
                Sequence == 0 ?
                TableDbLink :
                string.Format("{0} e{1}", TableDbLink, Sequence);
            public string ColumnExpr =>
                Sequence == 0 ?
                string.Format("{0}.{1}", Table, Column) :
                string.Format("e{0}.{1}", Sequence, Column);

            public string TableDbLink
            {
                get
                {
                    var link = "";
                    switch (this.AppName) { 
                        case "SPEL":
                            link = "@spel";
                            break;
                        case "SPELREF":
                            link = "@spelref";
                            break;
                        case "SPAPLANT":
                            link = "@plant";
                            break;
                        case "SPPID":
                            link = "@sppid";
                            break;
                        case "SPASITE":
                            link = "@site";
                            break;
                    }
                    return Table + link;
                }
            }
        }

        #endregion

        public ItemSQL(IServiceProvider provider)
        {
            this._provider = provider;
        }

        /// <summary>
        /// Calculate the ItemJoin object which contains all the data
        /// to generate the SQL statement for the Item. The ItemJoin
        /// has methods PrintAllSQL and PrintItemAttributions to print
        /// out the SQL statements.
        /// </summary>
        /// <param name="item">Item is an EntityFramework object</param>
        public Task<ItemJoin> GetItemJoin(Item item, IRepository _repo)
        {
            return Task.Factory.StartNew(() =>
            {
                // get all paths in the item
                var qPaths = item!.ItemAtts!
                    .Select(i => i.Path)
                    .Distinct()
                    .OrderBy(i => i)
                    .ToList();

                var pctx = Repository.GetPathContext(qPaths, _repo.AppName, _provider);
                pctx.SrcEntity = item.SourceEntity;
                pctx.ItemAttributions = item.ItemAtts.Where(i => i.Attribution != null).ToList();

                var attsNull = item.ItemAtts.Where(i => i.Attribution == null).ToList();
                var attbs = FindAttribution(attsNull, pctx, _repo);
                pctx.ItemAttributions.AddRange(attbs);

                // iterate through each path
                var lstJoin = new List<PathJoin>();

                foreach (var p in qPaths.Where(i => i != "0"))
                {
                    // preserve relationship ordering in the path
                    var pathJoin = GetPathJoin(p, pctx);
                    if (pctx.ItemRelationships.TryGetValue(p, out var irel))
                        pathJoin.ItemRelation = irel;
                    lstJoin.Add(pathJoin);
                }

                #region Base table

                // base table for path 0
                var t = new TblCol { Table = item.SourceEntity?.EntityName, AppName = item.SourceEntity?.EntityApps };
                var p0 = new PathJoin
                {
                    Path = "0",
                    Joins = new List<RelJoin>
                    {
                        new RelJoin
                        {
                            RelNumber = 0,
                            RelType = "",
                            Join = string.Format("from {0}", t.TableDbLink)
                        }
                    }
                };

                var atts0 = item.ItemAtts
                        .Where(i => i.Path == "0" && i.Attribution != null);

                //p0.SelectSQL = this.CalcPathSQL(atts0, p0, false);
                p0.SetPathSQL(atts0);
                lstJoin.Insert(0, p0);

                #endregion

                Attributes pkAtt;
                pctx.Attributes.TryGetValue(string.Format("{0}:{1}", item.SourceEntity.EntityApps, item.SourceEntity?.PrimaryKey), out pkAtt);
                return new ItemJoin
                {
                    ItemName = item.Name,
                    SourceTable = item.SourceEntity?.EntityName,
                    ItemID = item.ID,
                    SourceTableID = item.SourceEntity?.EntityNumber,
                    SourcePrimaryKey = pkAtt?.AttributeName,
                    Joins = lstJoin
                };
            });
        }

        /// <summary>
        /// Calculate a list of SQL table joins for the path
        /// </summary>
        /// <param name="path">Path is the data from Data Dictionary</param>
        public PathJoin GetPathJoin(string path, PathContext pctx)
        {
            var lstJoin = new List<RelJoin>();

            // preserve relationship ordering in the path
            var lstRelNo = path.Split("_").Select(i => Convert.ToInt32(i)).ToList();

            #region Base table

            // base table
            var beginTable = pctx.SrcEntity;
            if (pctx.SrcEntity == null)
                beginTable = pctx.Relationships[lstRelNo.First()].SourceEntityObj;

            var firstTable = new TblCol { Table = beginTable.EntityName, AppName = beginTable.EntityApps };
            lstJoin.Add(new RelJoin
            {
                RelNumber = 0,
                RelType = "",
                Join = string.Format("from {0}", firstTable.TableDbLink)
            });

            // first table in the join
            var lstTC = new List<TblCol> { firstTable };

            #endregion

            foreach (var relNo in lstRelNo)
            {
                if (pctx.GetRelationContext(relNo, out var rctx))
                {
                    var tc1 = new TblCol 
                    { 
                        Table = rctx.SrcEntities.EntityName,
                        Column = rctx.SrcAttr.AttributeName, 
                        AppName = rctx.Relationships.SourceSchema,
                        CorAttType = "S",
                        CorAttNo = rctx.Relationships.SourceCorAtt
                    };
                    var tc2 = new TblCol 
                    { 
                        Table = rctx.DestEntities.EntityName,
                        Column = rctx.DestAttr.AttributeName,
                        AppName = rctx.Relationships.DestSchema,
                        CorAttType = "D",
                        CorAttNo = rctx.Relationships.DestCorAtt
                    };

                    // establish which table/colum is on
                    // the left/right position of the join clause
                    var left = tc1;
                    var right = tc2;

                    // get the previous table in
                    // the multi-table joins
                    var prevTC = lstTC.Last();

                    if (left.Table != right.Table && left.Table == prevTC.Table)
                    {
                        left = tc2;
                        right = tc1;
                    }

                    // determine if the let/right tables in
                    // the current join require table alias(es)
                    if (lstTC.Any(e => e.Table == left.Table))
                        left.Sequence = lstTC.Count() + 1;
                    if (right.Table == prevTC.Table)
                        right.Sequence = prevTC.Sequence;

                    // track all tables in the
                    // multi-table joins
                    lstTC.Add(left);

                    lstJoin.Add(new RelJoin
                    {
                        RelNumber = rctx.Relationships.RelNumber,
                        RelType = rctx.Relationships.RelType,
                        LeftCorAttNo = left.CorAttNo,
                        LeftCorAttType = left.CorAttType,
                        RightCorAttNo = right.CorAttNo,
                        RightCorAttType = right.CorAttType,
                        Join = string.Format("inner join {0} on ({1} = {2})",
                            left.FromExpr, left.ColumnExpr, right.ColumnExpr)
                    });
                }
                else if (relNo < 0)
                {
                    var prevTbl = lstTC.Last();
                    var tbl = new TblCol { Table = "T_EquipmentOperatingCase", Column = "SP_EQUIPID" };
                    lstJoin.Add(new RelJoin
                    {
                        RelNumber = relNo,
                        RelType = "OPER_CASE",
                        Join = string.Format("inner join {0} on ({1} = {2})",
                            tbl.FromExpr, tbl.ColumnExpr, prevTbl.ColumnExpr)
                    });
                }
            }

            var lastTC = lstTC.Last();

            var p = new PathJoin 
            {
                Path = path,
                Joins = lstJoin,
                EntityName = lastTC.Table,
                Alias = lastTC.Sequence == 0 ? "" : string.Format("e{0}", lastTC.Sequence )
            };
            p.SetPathSQL(pctx.GetAttributions(path));
            return p;

        }

        protected List<ItemAttributions> FindAttribution(IEnumerable<ItemAttributions> attrs, PathContext pctx, IRepository repo)
        {
            var lst = new List<ItemAttributions>();
            var paths = attrs.GroupBy(a => a.Path);
            foreach ( var pg in paths)
            {
                var attbIds = pg.Select(i => i.AttributionID.Value).ToList();
                var relNo = Convert.ToInt32(pg.Key.Split('_').Last());
                if (!pctx.GetRelationContext(relNo, out var relCtx))
                    continue;
                var rel = relCtx.Relationships;

                var entityId = rel.SourceEntity.Value;
                var appName = rel.SourceSchema;
                if (repo.AppName == appName)
                {
                    entityId = rel.DestEntity.Value;
                    appName = rel.DestSchema;
                }

                var attbs = Repository.FindItemAttributions(entityId, attbIds, appName, _provider);
                foreach (var a in attbs)
                    a.Path = pg.Key;
                lst.AddRange(attbs);
            }
            return lst;
        }

        /// <summary>
        /// Calculate the ObjectJoin object which contains all the data
        /// to generate the SQL statement for the Item. The ObjectJoin
        /// has a method PrintAllSQL to print out the SQL statements.
        /// </summary>
        public ObjectJoin GetItemObjRelJoin(int itemId, IRepository repo)
        {
            var lstRel = repo.FindItemRelations(itemId).Result;
            if (lstRel.Count == 0)
                return null;

            var paths = lstRel.Select(e => e.Path!).Distinct().ToList();
            var pctx = Repository.GetPathContext(paths, repo.AppName, _provider);

            var childs = lstRel.Where(i => i.ParentItemID == itemId).OrderBy(i => i.Name).ToList();
            var parents = lstRel.Where(i => i.ChildItemID == itemId).OrderBy(i => i.Name).ToList();

            var q1 = childs.Select(i => i.ParentItem);
            var q2 = parents.Select(i => i.ChildItem);
            var item = q1.Concat(q2).First();

            var lstJoin = new List<PathJoin>();
            foreach (var p in childs.Concat(parents))
            {
                pctx.SrcEntity = p.ParentItem.SourceEntity;
                var pathJoin = GetPathJoin(p.Path, pctx);
                if (pctx.ItemRelationships.TryGetValue(p.Path, out var irel))
                    pathJoin.ItemRelation = irel;
                lstJoin.Add(pathJoin);
            }

            return new ObjectJoin
            {
                ItemName = item.Name,
                SourceTable = item.SourceEntity?.EntityName,
                Joins = lstJoin
            };
        }
    }
}
