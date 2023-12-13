using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    public record Applications
    {
        public int ID { get; set; } = 0;
        public int FilterInstanceID { get; set; } = 0;
        public int CategoryID { get; set; } = 0;
    }
}
