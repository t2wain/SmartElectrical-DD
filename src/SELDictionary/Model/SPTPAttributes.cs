using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    public record SPTPAttributes
    {
        #region Columns

        public int ID { get; set; }
        public int? SPTPLayoutsID { get; set; }
        public int? AttributeID { get; set; }
        public string? ColumnName { get; set; }
        public string? Position { get; set; }
        public int? ColumnWidth { get; set; }

        #endregion

        public ItemAttributions? ItemAttribute { get; set; }
    }
}
