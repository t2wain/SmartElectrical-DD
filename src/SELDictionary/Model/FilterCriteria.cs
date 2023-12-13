using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    public record FilterCriteria
    {
        public int ID { get; set; }
        public int? FilterDefinitionID { get; set; }
        public int? SourceAttribute { get; set; }
        public string? Operator { get; set; }
        public string? Conjunctive { get; set; }
        public string? AttributeValue { get; set; }
    }
}
