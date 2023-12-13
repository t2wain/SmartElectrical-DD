using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    /// <summary>
    /// Describe a database column
    /// </summary>
    public record Attributes
    {
        #region Columns

        public int AttributeNumber { get; set; }
        public string AttributeName { get; set; } = "";
        public string? AttributeDataType { get; set; }
        public string? AttributeDefValue { get; set; }
        public int? AttributeUOM { get; set; }
        public string? AttributeDescription { get; set; }
        public string? AttributeFormat { get; set; }
        public string? AttributeCodeListed { get; set; }
        public string? AttributeDisplay { get; set; }
        public int? DependsOnID { get; set; }
        public string? Marker { get; set; }
        public int? AttributeUOMID { get; set; }

        #endregion


    }
}
