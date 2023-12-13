using SELDictionary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary
{
    /// <summary>
    /// This is the data object calculated
    /// from the ItemSQL class for an Item.
    /// </summary>
    public record ItemJoin
    {
        public string? ItemName { get; set; }
        public string? SourceTable { get; set; }
        public int? ItemID { get; set; }
        public int? SourceTableID { get; set; }
        public string? SourcePrimaryKey { get; set; }

        /// <summary>
        /// Result of the calculation by the 
        /// ItemSQL class.
        /// </summary>
        public List<PathJoin>? Joins { get; set; }

        /// <summary>
        /// Output the complete SQL statements for this Item
        /// </summary>
        public void PrintAllSQL(TextWriter? writer = null)
        {
            var output = Console.Out;
            if (writer != null)
                output = writer;

            output.WriteLine(string.Format("Item : {0} ({1}), Entity : {2} ({3})\n", 
                ItemName, ItemID, SourceTable, SourceTableID));

            // group joins by relation type SUBCLASS vs. ASSOC_NC
            var q1 = this.Joins.Where(i => i.Joins.Any(j => j.RelType == "ASSOC_NC"));
            var q2 = this.Joins.Where(i => !i.Joins.Any(j => j.RelType == "ASSOC_NC"));

            foreach (var p in q2.Concat(q1))
            {
                var relName = "";
                if (p.ItemRelation != null)
                    relName = string.Format(" -  ( {0} )", p.ItemRelation.Name);
                output.WriteLine(string.Format("Path : {0}{1}", p.Path, relName));
                output.WriteLine(p.SelectSQL);
            }

            output.WriteLine(new string('=', 10));
            output.WriteLine();
        }

        /// <summary>
        /// Output the properties of an Item
        /// </summary>
        /// <param name="item">Item is EntityFramework object</param>
        public static void PrintItemAttributions(Item item, TextWriter? writer = null)
        {
            var output = Console.Out;
            if (writer != null) output = writer;

            output.WriteLine(string.Format("Item : {0} ({1}), Entity : {2} ({3})\n",
                item.Name, item.ID, item.SourceEntity.EntityName, item.SourceEntity.EntityNumber));

            var q1 = item.ItemAtts.Where(e => !e.Name.Contains(".")).OrderBy(e => e.Name);
            var q2 = item.ItemAtts.Where(e => e.Name.Contains(".")).OrderBy(e => e.Name);
            var q = q1.Concat(q2);
            foreach (var a in q)
                output.WriteLine("{0} - {1}", a.Name, a.Path);

            output.WriteLine();
            output.WriteLine(new string('=', 10));
            output.WriteLine();
        }
    }

}
