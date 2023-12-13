using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.Model
{
    public record CodeLists
    {
        public int CodeListNumber { get; set; }
        public int CodeListIndex { get; set; }
        public string? CodeListText { get; set; }
        public string? CodeListShortText { get; set; }
        public int? CodeListConstraint { get; set; }
        public int? CodeListSortValue { get; set; }
        public int? CodeListEntryDisabled { get; set; }
    }
}
