using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    public record SPSorts
    {
        public int ID { get; set; }
        public int SPTPLayoutsID { get; set; }
        public int AttributeID { get; set; }
        public int? OrderType { get; set; }
        public int? SortType { get; set; }
    }
}
