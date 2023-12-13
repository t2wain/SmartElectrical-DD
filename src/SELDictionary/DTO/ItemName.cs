using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.DTO
{
    public record ItemName
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string AppName { get; set; }
    }
}
