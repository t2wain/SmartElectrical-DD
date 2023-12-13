using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    /// <summary>
    /// Associate a database column to a database table
    /// </summary>
    public record UniqueAtts
    {
        #region Columns

        public int ID { get; set; }
        /// <summary>
        /// Database table ID
        /// </summary>
        public int EntityNumber { get; set; }
        /// <summary>
        /// Database column ID
        /// </summary>
        public int AttributeNumber { get; set; }
        public string UniqueName { get; set; } = "";
        public string? DisplayName { get; set; }
        public string? Display { get; set; }
        public string? Filter { get; set; }
        public string? CalculationProgID { get; set; }
        public string? ValidationProgID { get; set; }
        public int? Category { get; set; }
        public string? Marker { get; set; }
        public int? ReadOnlyMask { get; set; }

        #endregion

        public Entities? Entity { get; set; }
        public Attributes? Attribute { get; set; }
    }
}
