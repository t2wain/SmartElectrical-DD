using SELDictionary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary
{
    /// <summary>
    /// This is a data object class calculated by the
    /// ItemSQL class.
    /// </summary>
    public record PathJoin
    {
        public string? Path { get; set; }
        public string? Alias { get; set; }
        public string? EntityName { get; set; }
        public List<RelJoin>? Joins { get; set; }
        public string? SelectSQL { get; set; }
        public SourceDestObjectRels? ItemRelation { get; set; }
        protected string ColExpr(ItemAttributions ia, bool isLast)
        {
            var u = ia.Attribution;
            var t = string.IsNullOrEmpty(this.Alias) ? u.Entity?.EntityName : this.Alias;
            return string.Format("   {0}.{1} {2}{3} -- {4}",
                t,
                u.Attribute?.AttributeName,
                ia.Name.Replace(".", "_"),
                isLast ? "" : ",",
                this.ColComment(u));
        }
        protected string ColComment(UniqueAtts u)
        {
            var a = u.Attribute;
            return string.Format("T({0}), F({1}), V({2})",
                a.AttributeDataType,
                (a.AttributeFormat ?? "").ToLower().Contains("variable length") ? "Text" : a.AttributeFormat,
                a.AttributeDisplay ?? "T");
        }
        public string SetPathSQL(IEnumerable<ItemAttributions> atts)
        {
            var sb = new StringBuilder();

            if (atts == null || atts.Count() == 0)
                sb.AppendLine(string.Format("select {0}.* ",
                    string.IsNullOrEmpty(this.Alias) ? EntityName : this.Alias));
            else
            {
                sb.AppendLine("select");
                var lstAtt = new List<string>();
                // iterate through each database columns
                // in the select statement
                var la = atts.Last();
                foreach (var a in atts)
                    lstAtt.Add(this.ColExpr(a, a == la));
                sb.AppendLine(string.Join("\n", lstAtt.ToArray()));
            }

            sb.AppendLine(this.Joins!.First().Join);

            // iterate through each table join
            foreach (var r in this.Joins!.Skip(1))
            {
                var j = string.Format("   {0}{1}", r.Join, r.Comment);
                sb.AppendLine(j);
            }

            this.SelectSQL = sb.ToString();
            return this.SelectSQL;
        }
    }

}
