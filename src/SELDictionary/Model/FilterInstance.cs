using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    public record FilterInstance
    {
        public int ID { get; set; }
        public int? FilterDefinitionID { get; set; }
        public int? ParentFilterID { get; set; }
        public string? ShortName { get; set; }
        public int? InstanceType { get; set; }
    }
}
