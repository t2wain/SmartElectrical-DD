using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SELDictionary.DTO;
using SELDictionary.Model;

namespace SELDictionary
{
    public class Repository : ISPELDRepository, IPLANTDRepository, ISITEDRepository
    {

        #region Query across multiple Repository

        private static IReadOnlyCollection<string> APPNAMES = new string[] { "SPEL", "SPELREF", "SPAPLANT", "SPSITE" };

        private static IRepository? GetRepository(string appName, IServiceProvider provider)
        {
            IRepository? repo = null;
            switch (appName)
            {
                case "SPEL":
                case "SPELREF":
                    repo = provider.GetService<ISPELDRepository>();
                    break;
                case "SPAPLANT":
                    repo = provider.GetService<IPLANTDRepository>();
                    break;
                case "SPSITE":
                    repo = provider.GetService<ISITEDRepository>();
                    break;
            }
            return repo;
        }

        public static Dictionary<string, Attributes> FindAttributesFromRelations(IEnumerable<Relationships> rels, IServiceProvider provider)
        {
            var dictAtt = new Dictionary<string, Attributes>();
            foreach (var app in APPNAMES)
            {
                var qRel1 = rels.Where(r => r.SourceSchema == app);
                var qRel2 = rels.Where(r => r.DestSchema == app);
                var qRel = qRel1.Concat(qRel2).Distinct().ToList();
                if (qRel.Count() == 0) continue;

                IRepository? repo = GetRepository(app, provider);
                var atts = repo.FindAttributesFromRelations(qRel).Result
                    .Select(i => new { Attr = i, Index = string.Format("{0}:{1}", app, i.AttributeNumber) });
                foreach (var a in atts)
                    if (!dictAtt.ContainsKey(a.Index))
                        dictAtt.Add(a.Index, a.Attr);
            }
            return dictAtt;
        }

        public static Dictionary<string, Entities> FindEntitiesFromRelations(IEnumerable<Relationships> rels, IServiceProvider provider)
        {
            var q1 = rels.Where(r => r.SourceEntityObj != null).Select(r => r.SourceEntityObj);
            var q2 = rels.Where(r => r.DestEntityObj != null).Select(r => r.DestEntityObj);
            var dictEnt = q1.Concat(q2).Distinct()
                .ToDictionary(i => string.Format("{0}:{1}", i.EntityApps, i.EntityNumber));

            foreach (var app in APPNAMES)
            {
                var qEnt1 = rels.Where(r => r.SourceSchema == app && r.SourceEntityObj == null).Select(i => i.SourceEntity.Value);
                var qEnt2 = rels.Where(r => r.DestSchema == app && r.DestEntityObj == null).Select(i => i.DestEntity.Value);
                var qEnt = qEnt1.Concat(qEnt2).Distinct().ToList();
                if (qEnt.Count() == 0) continue;

                IRepository? repo = GetRepository(app, provider);
                var ents = repo.FindEntity(qEnt).Result
                    .Select(i => new { Entity = i, Index = string.Format("{0}:{1}", app, i.EntityNumber) });
                foreach (var e in ents)
                    if (!dictEnt.ContainsKey(e.Index))
                        dictEnt.Add(e.Index, e.Entity);
            }
            return dictEnt;
        }

        public static List<ItemAttributions> FindItemAttributions(int entityId, IEnumerable<int> itemAttbIds, string appName, IServiceProvider provider)
        {
            IRepository? repo = GetRepository(appName, provider);
            return repo.FindItemAttributions(entityId, itemAttbIds).Result;
        }

        public static PathContext GetPathContext(IEnumerable<string> paths, string appName, IServiceProvider provider)
        {
            IRepository? repo = GetRepository(appName, provider);
            var dictRel = repo.FindRelationshipsFromPaths(paths).Result.ToDictionary(i => i.RelNumber);
            var dictAtt = Repository.FindAttributesFromRelations(dictRel.Values, provider);

            var dictItemRel = new Dictionary<string, SourceDestObjectRels>();
            var res = repo.FindItemRelationsFromPaths(paths).Result;
            foreach (var r in res)
                if (!dictItemRel.ContainsKey(r.Path))
                    dictItemRel.Add(r.Path, r);

            var dictEntity = Repository.FindEntitiesFromRelations(dictRel.Values, provider);
            return new PathContext
            {
                Relationships = dictRel,
                Attributes = dictAtt,
                ItemRelationships = dictItemRel,
                Entities = dictEntity
            };
        }

        #endregion


        private readonly IDictDbContextReadOnly _db;
        private readonly string _appName;

        public Repository(IDictDbContextReadOnly db, string appName)
        {
            this._db = db;
            this._appName = appName;
        }

        public string AppName => this._appName;

        public Task<Entities?> FindEntity(int entityNumber) =>
            _db.Entities
                .Include(e => e.EntityAttrs!)
                .ThenInclude(e => e.Attribute)
                .Where(e => e.EntityNumber == entityNumber)
                .FirstOrDefaultAsync();
        
        public Task<List<Entities>> FindEntity(IEnumerable<int> entityNumbers) =>
            _db.Entities
                .Include(e => e.EntityAttrs!)
                .ThenInclude(e => e.Attribute)
                .Where(e => entityNumbers.Any(i => i == e.EntityNumber))
                .ToListAsync();
        
        public Task<List<ItemName>> GetAllItemNames() =>
            _db.Items
                .OrderBy(e => e.Name)
                .Select(e => new ItemName { ItemID = e.ID, Name = e.Name, AppName = _appName })
                .ToListAsync();

        public Task<Item?> FindItem(int itemID) =>
            _db.Items
                .Include(e => e.SourceEntity!)
                .Include(e => e.ItemAtts!).ThenInclude(e => e.Attribution).ThenInclude(e => e.Attribute)
                .Include(e => e.ItemAtts!).ThenInclude(e => e.Attribution).ThenInclude(e => e.Entity)
                .Where(e => e.ID == itemID)
                .FirstOrDefaultAsync();
        
        public Task<List<ItemAttributions>> FindItemAttributions(int entityId, IEnumerable<int> itemAttbIds)
        {
            var qItems = _db.ItemAtts
                .Where(e => e.ItemID.HasValue && e.AttributionID.HasValue && e.Path == "0")
                .Include(e => e.Attribution).ThenInclude(e => e.Attribute)
                .Include(e => e.Attribution).ThenInclude(e => e.Entity);

            var q = from a in qItems
                    join e in _db.Items on a.ItemID.Value equals e.ID
                    where e.SourceTable == entityId  && itemAttbIds.Any(i => a.AttributionID.Value == i)
                    select a;

            return q.ToListAsync();
        }

        public Task<List<Relationships>> FindRelationships(IEnumerable<int> rels) =>
            _db.Relationships
                .Include(e => e.SourceEntityObj)
                .Include(e => e.DestEntityObj)
                .Include(e => e.SourceCorAttObj)
                .Include(e => e.DestCorAttObj)
                .Where(e => rels.Contains(e.RelNumber))
                .ToListAsync();

        public Task<List<Relationships>> FindRelationshipsFromPaths(IEnumerable<string> paths)
        {
            var qRelNos = paths.SelectMany(i => i.Split("_")).Distinct().Select(i => Convert.ToInt32(i));
            return this.FindRelationships(qRelNos.ToList());
        }

        public Task<List<Attributes>> FindAttributes(IEnumerable<int> attNos) =>
            _db.Attributes
                .Where(i => attNos.Contains(i.AttributeNumber))
                .ToListAsync();

        public Task<List<Attributes>> FindAttributesFromRelations(IEnumerable<Relationships> rels)
        {
            var qAttId1 = rels.Select(i => i.SourceCorAttObj?.Attributes);
            var qAttId2 = rels.Select(i => i.DestCorAttObj?.Attributes);
            var lstAttId = qAttId1.Concat(qAttId2).Distinct().Select(i => Convert.ToInt32(i)).ToList();
            return this.FindAttributes(lstAttId);
        }

        public Task<List<SourceDestObjectRels>> FindItemRelations(int itemId) =>
            _db.SourceDestObjectRels
                .Include(i => i.ParentItem).ThenInclude(i => i.SourceEntity)
                .Include(i => i.ChildItem).ThenInclude(i => i.SourceEntity)
                .Where(i => i.ParentItemID == itemId || i.ChildItemID == itemId)
                .ToListAsync();

        public Task<List<SourceDestObjectRels>> FindItemRelationsFromPaths(IEnumerable<string> paths) =>
            _db.SourceDestObjectRels
                .Include(i => i.ParentItem)
                .Include(i => i.ChildItem)
                .Where(i => paths.Any(e => e == i.Path))
                .ToListAsync();

        public Task<Enumerations?> FindEnumerations(int enumId) =>
            _db.Enumerations
                .Include(e => e.CodeItems)
                .Where(e => e.ID == enumId)
                .FirstOrDefaultAsync();

        public Task<List<SPTPViews>> FindTableView(IEnumerable<int> itemIds) =>
            _db.SPTPViews
                .Include(e => e.Item)
                .Include(e => e.ParentView)
                .Include(e => e.DefaultViewLayout)
                .Include(e => e.ViewLayouts)
                    .ThenInclude(e => e.ViewAttributes)
                    .ThenInclude(e => e.ItemAttribute)
                .Where(e => itemIds.Any(i => i == (e.ItemType ?? 0)))
                .ToListAsync();
    }
}
