using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    public record Enumerations
    {
        #region Columns

        public int ID { get; set; }
        public string? DependsOnID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? DisplayUsage { get; set; }

        #endregion

        public List<CodeLists>? CodeItems { get; set; }
    }
}
