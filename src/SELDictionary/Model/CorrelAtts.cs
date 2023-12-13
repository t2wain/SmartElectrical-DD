using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    /// <summary>
    /// Describe database column with role as primary key or foreign key
    /// </summary>
    public record CorrelAtts
    {
        public int CorrelAttNumber { get; set; }
        public int? NumberOfAtts { get; set; }
        /// <summary>
        /// Concatenated attribute IDs
        /// </summary>
        public string? Attributes { get; set; }
        public string? Description { get; set; }
        public string? Marker { get; set; }
    }
}
