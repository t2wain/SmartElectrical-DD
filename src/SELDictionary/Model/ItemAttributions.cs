using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    /// <summary>
    /// Describe business object property
    /// </summary>
    public record ItemAttributions
    {
        #region Columns

        public int ID { get; set; } = 0;
        public int? ItemID { get; set; }
        /// <summary>
        /// Referencing UniqueAtts which refers to the database table 
        /// and column that contains the data for this property
        /// </summary>
        public int? AttributionID { get; set; }
        /// <summary>
        /// Concatenated Relationships which define 
        /// the sequence of database table joins
        /// </summary>
        public string? Path { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Marker { get; set; }
        public int? Category { get; set; }
        public int? Manual { get; set; } = 1;
        public int? ReadOnlyMask { get; set; } = 0;
        public int? Discard { get; set; }

        #endregion

        /// <summary>
        /// Define the database table and column 
        /// that contains the data for this property.
        /// </summary>
        public UniqueAtts? Attribution { get; set; }
    }
}
