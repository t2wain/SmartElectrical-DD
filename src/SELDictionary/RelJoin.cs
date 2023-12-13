using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary
{
    /// <summary>
    /// This is a data object class calulated by
    /// the ItemSQL class.
    /// </summary>
    public record RelJoin
    {
        public int RelNumber { get; set; }
        public string? RelType { get; set; }
        public string? LeftCorAttType { get; set; }
        public int? LeftCorAttNo { get; set; }
        public string? RightCorAttType { get; set; }
        public int? RightCorAttNo { get; set; }
        public string? Join { get; set; }
        public string? Comment =>
            String.Format(" -- RN({0}), RT({1}), {2}C({3}), {4}C({5})", 
                RelNumber, RelType,
                LeftCorAttType, LeftCorAttNo,
                RightCorAttType, RightCorAttNo);
    }

}
