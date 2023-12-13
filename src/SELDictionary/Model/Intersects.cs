using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    public record Intersects
    {
        public string? RelType { get; set; }
        public int ChildRel { get; set; }
        public int ParentRel { get; set; }
        public string? Description { get; set; }
        public string? Marker { get; set; }
    }
}
