using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    public record FilterRelationship
    {
        public int ID { get; set; }
        public int RelationshipID { get; set; }
        public int AttributeID { get; set; }
        public string? AttributeValue { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public int? ItemID { get; set; }
        public string? SPCreate { get; set; }
    }
}
