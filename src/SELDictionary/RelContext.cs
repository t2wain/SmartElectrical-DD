using SELDictionary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary
{
    /// <summary>
    /// This is a data object class calculated by
    /// The PathContext class.
    /// </summary>
    public class RelContext
    {
        public Relationships Relationships { get; set; }
        public Attributes SrcAttr { get; set; }
        public Attributes DestAttr { get; set; }
        public Entities SrcEntities { get; set; }
        public Entities DestEntities { get; set; }
    }

}
