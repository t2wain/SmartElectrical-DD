using SELDictionary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SELDictionary.ItemSQL;

namespace SELDictionary
{
    /// <summary>
    /// This class is calculated by the Repository and
    /// contains all the data from the DbContext for
    /// the calculation in ItemSQL.
    /// </summary>
    public class PathContext
    {
        public Entities SrcEntity { get; set; }
        public IDictionary<int, Relationships> Relationships { get; set; }
        public IDictionary<string, Attributes> Attributes { get; set; }
        public IDictionary<string, SourceDestObjectRels> ItemRelationships { get; set; }
        public IDictionary<string, Entities> Entities { get; set; }
        public List<ItemAttributions> ItemAttributions { get; set; }

        public bool GetRelationContext(int relNo, out RelContext relCtx)
        {
            relCtx = null;
            if (Relationships.TryGetValue(relNo, out var rel)
                && Attributes.TryGetValue(string.Format("{0}:{1}", rel.SourceSchema, rel.SourceCorAttObj?.Attributes), out var srcAtt)
                && Attributes.TryGetValue(string.Format("{0}:{1}", rel.DestSchema, rel.DestCorAttObj?.Attributes), out var destAtt)
                && Entities.TryGetValue(string.Format("{0}:{1}", rel.SourceSchema, rel.SourceEntity), out var srcEnt)
                && Entities.TryGetValue(string.Format("{0}:{1}", rel.DestSchema, rel.DestEntity), out var destEnt))
            {
                relCtx = new RelContext
                {
                    Relationships = rel,
                    SrcAttr = srcAtt,
                    DestAttr = destAtt,
                    SrcEntities = srcEnt,
                    DestEntities = destEnt
                };
                return true;
            }
            else { return false; }
        }

        public IEnumerable<ItemAttributions> GetAttributions(string path)
        {
            if (ItemAttributions == null)
                return new List<ItemAttributions>();
            else return ItemAttributions.Where(i => i.Path == path && i.Attribution != null);
        }
    }

}
