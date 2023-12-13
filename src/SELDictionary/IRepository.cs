using SELDictionary.DTO;
using SELDictionary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary
{
    public interface IRepository
    {
        string AppName { get; }
        Task<Entities?> FindEntity(int entityNumber);
        Task<List<Entities>> FindEntity(IEnumerable<int> entityNumbers);
        Task<List<ItemName>> GetAllItemNames();
        Task<Item?> FindItem(int itemID);
        Task<List<ItemAttributions>> FindItemAttributions(int entityId, IEnumerable<int> itemAttbIds);
        Task<List<Relationships>> FindRelationships(IEnumerable<int> rels);
        Task<List<Relationships>> FindRelationshipsFromPaths(IEnumerable<string> paths);
        Task<List<Attributes>> FindAttributes(IEnumerable<int> attNos);
        Task<List<Attributes>> FindAttributesFromRelations(IEnumerable<Relationships> rels);
        Task<List<SourceDestObjectRels>> FindItemRelations(int itemId);
        Task<List<SourceDestObjectRels>> FindItemRelationsFromPaths(IEnumerable<string> paths);
        Task<Enumerations?> FindEnumerations(int enumId);
        Task<List<SPTPViews>> FindTableView(IEnumerable<int> itemIds);
    }

    public interface IPLANTDRepository : IRepository { }

    public interface ISPELDRepository : IRepository { }
    public interface ISITEDRepository : IRepository { }
}
