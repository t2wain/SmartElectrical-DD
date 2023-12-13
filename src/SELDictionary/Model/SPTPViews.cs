using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    public record SPTPViews
    {
        #region Column

        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ItemType { get; set; }
        public int? DefaultFilter { get; set; }
        public int? DefaultLayout { get; set; }
        public string? TemplateName { get; set; }
        public int? ParentID { get; set; }
        public int? AppUsage { get; set; }
        public int? AppID { get; set; }

        #endregion

        public Item? Item { get; set; }
        public SPTPViews? ParentView { get; set; }
        public SPTPLayouts? DefaultViewLayout { get; set; }
        public List<SPTPLayouts>? ViewLayouts { get; set; }
    }
}
