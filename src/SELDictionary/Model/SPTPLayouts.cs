using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    public record SPTPLayouts
    {
        #region Columns

        public int ID { get; set; }
        public int SPTPViewsID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? DisplayStyle { get; set; }
        public int? OwnerID { get; set; }
        public int AppUsage { get; set; }

        #endregion

        public List<SPTPAttributes>? ViewAttributes { get; set; }
    }
}
