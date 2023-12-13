using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    public record Category
    {
        public int ID { get; set; } = 0;
        public int ParentCategoryID { get; set; } = 0;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ApplicationID { get; set; } = 0;
        public string? UserName { get; set; }
    }
}
