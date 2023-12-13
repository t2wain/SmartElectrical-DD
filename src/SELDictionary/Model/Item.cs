using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    /// <summary>
    /// Define a logical business object
    /// </summary>
    public record Item
    {
        #region Columns

        public int ID { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        /// <summary>
        /// Associate to a database table
        /// </summary>
        public int SourceTable { get; set; }
        public string? WhereClause { get; set; }
        public string? AppSchema { get; set; }
        public int? ParentID { get; set; }
        public string? RelationPath { get; set; }
        public int? OwnerID { get; set; }
        public string? ValidationProgID { get; set; }
        public string? Marker { get; set; }
        public int? Instancing { get; set; }
        public string? Display { get; set; }
        public int? SPUndo { get; set; }

        #endregion

        /// <summary>
        /// Database table
        /// </summary>
        public Entities? SourceEntity { get; set; }
        /// <summary>
        /// Object properties
        /// </summary>
        public ICollection<ItemAttributions>? ItemAtts { get; set; }
    }
}
