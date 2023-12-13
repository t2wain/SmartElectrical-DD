using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary
{
    /// <summary>
    /// This is the data object calculated by the
    /// ItemSQL class for an Item.
    /// </summary>
    public class ObjectJoin
    {
        public string? ItemName { get; set; }
        public string? SourceTable { get; set; }

        /// <summary>
        /// Result of the calcuation by ItemSQL class.
        /// </summary>
        public List<PathJoin>? Joins { get; set; }

        public void PrintAllSQL(TextWriter? writer = null)
        {
            var output = Console.Out;
            if (writer != null)
                output = writer;

            output.WriteLine(string.Format("Item : {0}\n", this.ItemName));

            // group joins by relation type SUBCLASS vs. ASSOC_NC
            //var q1 = this.Joins.Where(i => i.Joins.Any(j => j.RelType == "ASSOC_NC"));
            //var q2 = this.Joins.Where(i => !i.Joins.Any(j => j.RelType == "ASSOC_NC"));

            foreach (var p in Joins)
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
        public static void PrintRelations(IEnumerable<int> itemIds, 
            IRepository _repo, TextWriter? writer = null)
        {
            var output = Console.Out;
            if (writer != null)
                output = writer;

            foreach (var id in itemIds)
            {
                var rels = _repo.FindItemRelations(id).Result;
                if (rels.Count == 0) continue;

                var item1 = rels.Where(e => e.ParentItemID == id).Select(e => e.ParentItem);
                var item2 = rels.Where(e => e.ChildItemID == id).Select(e => e.ChildItem);
                var item = item1.Concat(item2).First();
                output.WriteLine(string.Format("Item : {0}\n", item.Name));

                var parents = rels.Where(e => e.ParentItemID == id).OrderBy(e => e.Name).ToList();
                var children = rels.Where(e => e.ChildItemID == id).OrderBy(e => e.Name).ToList();
                if (parents.Count > 0)
                {
                    output.WriteLine("Parent :\n");
                    foreach (var i in parents)
                        output.WriteLine(string.Format("   {0} - {1}", i.Name, i.Path));
                }

                output.WriteLine();

                if (children.Count > 0)
                {
                    output.WriteLine("Child :\n");
                    foreach (var i in children)
                        output.WriteLine(string.Format("   {0} - {1}", i.Name, i.Path));
                }

                output.WriteLine();
                output.WriteLine(new string('=', 10));
                output.WriteLine();
            }
        }
    }
}
