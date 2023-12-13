using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    public record FilterDefinition
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? AppUsage { get; set; }
        public int? Conjunctive { get; set; }
        public int? ItemType { get; set; }
        public int? FilterType { get; set; }
        public int? ApplicationID { get; set; }
        public int? OwnerID { get; set; }
        public string? FilterUID { get; set; }
    }
}
