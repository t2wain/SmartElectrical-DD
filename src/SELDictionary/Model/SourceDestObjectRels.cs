using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    public record SourceDestObjectRels
    {
        #region Columns

        public int ID { get; set; }
        public int ParentItemID { get; set; }
        public int ChildItemID { get; set; }
        public string Path { get; set; } = string.Empty;
        public string? Marker { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? DisplayName { get; set; }

        #endregion

        public Item? ChildItem { get; set; }

        public Item? ParentItem { get; set; }
    }
}
