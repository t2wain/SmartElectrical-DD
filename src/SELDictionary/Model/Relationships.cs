using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    /// <summary>
    /// Define relationship of database tables in a join
    /// </summary>
    public record Relationships
    {
        #region Column

        public int RelNumber { get; set; }
        /// <summary>
        /// ASSOC_NC, GROUP, SUBCLASS
        /// </summary>
        public string? RelType { get; set; }
        /// <summary>
        /// Database table ID
        /// </summary>
        public int? SourceEntity { get; set; }
        /// <summary>
        /// See CorrelAtts class
        /// </summary>
        public int? SourceCorAtt { get; set; }
        /// <summary>
        /// Database table ID
        /// </summary>
        public int? DestEntity { get; set; }
        /// <summary>
        /// See CorrelAtts class
        /// </summary>
        public int? DestCorAtt { get; set; }
        public int? PositionAtt { get; set; }
        public int? TypeAttNumber { get; set; }
        public string? TypeValue { get; set; }
        public string? OneToOne { get; set; }
        public string? SourceSchema { get; set; }
        public string? DestSchema { get; set; }
        public string? Marker { get; set; }

        #endregion

        public Entities? SourceEntityObj { get; set; }
        public CorrelAtts? SourceCorAttObj { get; set; }
        public Entities? DestEntityObj { get; set; }
        public CorrelAtts? DestCorAttObj { get; set; }
    }
}
