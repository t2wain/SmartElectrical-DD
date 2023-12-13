using System.Globalization;
using System.Xml.Linq;

namespace SELImportManager
{
    public class ProjectXML
    {
        #region Class

        public record RelLink
        {
            public string Name { get; set; }
            public string Source { get; set; }
            public string ParentEntity { get; set; }
            public List<string> ParentBinds { get; set; }
            public string ChildEntity { get; set; }
            public List<string> ChildBinds { get; set; }

        }

        public record DataLink
        {
            public string Name { get; set; }
            public string Source { get; set; }
            public string Target { get; set; }
            public string Type { get; set; }
            public List<string> SQL { get; set; }

            public void Print(TextWriter output)
            {
                output.WriteLine(string.Format("{0} Name: {1}", Type, Name));
                output.WriteLine(string.Format("Source: {0}", Source));
                output.WriteLine(string.Format("Target: {0}", Target));
                foreach (var q in SQL)
                    output.WriteLine(string.Format("   {0}", q));
            }
        }

        public record GroupLink
        {
            public string GroupName { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public int Sequence { get; set; }
        }

        public class Links
        {
            public string FileName { get; set; }
            public List<RelLink> Relation { get; set; }
            public List<DataLink> Data { get; set; }
            public List<DataLink> Delete { get; set; }
            public List<DataLink> SelectList { get; set; }
            public List<GroupLink> GroupLink { get; set; }
            public void Print(TextWriter? writer = null)
            {
                var output = Console.Out;
                if (writer != null) output = writer;

                output.WriteLine(string.Format("File: {0}", FileName));
                output.WriteLine();

                if (GroupLink.Count > 0)
                {
                    output.WriteLine("Groups:");
                    output.WriteLine();
                    var groups = GroupLink.GroupBy(i => i.GroupName);
                    foreach (var g in groups)
                    {
                        output.WriteLine(string.Format("Group Name: {0}", g.Key));
                        foreach (var l in g.OrderBy(e => e.Sequence).ThenBy(e => e.Name))
                        {
                            output.WriteLine(string.Format("   Link: {0} : Type={1} : Seq={2}",
                                l.Name, l.Type, l.Sequence));
                        }
                        output.WriteLine();
                    }
                    output.WriteLine(new string('=', 10));
                    output.WriteLine();
                }

                if (Data.Count > 0)
                {
                    output.WriteLine("Data Links: ");
                    output.WriteLine();
                    foreach (var d in Data)
                    {
                        d.Print(output);
                        output.WriteLine();
                    }
                    output.WriteLine(new string('=', 10));
                    output.WriteLine();
                }

                if (Relation.Count > 0)
                {
                    output.WriteLine("Relation Links:");
                    output.WriteLine();
                    foreach (var rel in Relation)
                    {
                        output.WriteLine(string.Format("Relation Name: {0}", rel.Name));
                        output.WriteLine(string.Format("Source: {0}", rel.Source));
                        output.WriteLine(string.Format("Parent: {0}", rel.ParentEntity));
                        foreach (var b in rel.ParentBinds)
                            output.WriteLine(String.Format("   {0}", b));
                        output.WriteLine(string.Format("Child: {0}", rel.ChildEntity));
                        foreach (var b in rel.ChildBinds)
                            output.WriteLine(String.Format("   {0}", b));
                        output.WriteLine();
                    }
                    output.WriteLine(new string('=', 10));
                    output.WriteLine();
                }

                if (Delete.Count > 0)
                {
                    output.WriteLine("Delete Links: ");
                    output.WriteLine();
                    foreach (var d in Delete)
                    {
                        d.Print(output);
                        output.WriteLine();
                    }
                    output.WriteLine(new string('=', 10));
                    output.WriteLine();
                }

                if (SelectList.Count > 0)
                {
                    output.WriteLine("SelectList Links:");
                    output.WriteLine();
                    foreach (var sel in SelectList)
                    {
                        sel.Print(output);
                        output.WriteLine();
                    }
                    output.WriteLine(new string('=', 10));
                    output.WriteLine();
                }
            }
        }

        #endregion

        /// <summary>
        /// Parse the import XML project file to
        /// return a collection of links.
        /// </summary>
        /// <param name="fileName">Path to the XML project file</param>
        /// <returns>Collection of links</returns>
        public Links GetLinks(string fileName)
        {
            var f = new FileInfo(fileName);
            var doc = XElement.Load(f.FullName);
            var l = new Links
            {
                FileName = f.Name,
                Relation = GetRelationLinks(doc),
                Data = GetDataLinks(doc),
                Delete = GetDeleteLinks(doc),
                SelectList = GetSelectLinks(doc),
                GroupLink = GetGroupLinks(doc)
            };
            return l;
        }

        protected List<RelLink> GetRelationLinks(XElement doc)
        {
            var rels = (from l in doc.Elements("Link").Where(n => n.Attribute("Type").Value == "Relation")
                        let r = l.Element("Relation")
                        let p = r.Element("Parent")
                        let c = r.Element("Child")
                        select new RelLink
                        {
                            Name = l.Attribute("Name").Value,
                            Source = l.Element("Source").Value,
                            ParentEntity = p.Attribute("Entity").Value,
                            ParentBinds = p.Elements("Bind")
                              .Select(n => string.Format("{0} => {1}", n.Attribute("Source").Value, n.Attribute("Target").Value)).ToList(),
                            ChildEntity = c.Attribute("Entity").Value,
                            ChildBinds = c.Elements("Bind")
                              .Select(n => string.Format("{0} => {1}", n.Attribute("Source").Value, n.Attribute("Target").Value)).ToList()
                        })
                      .ToList();

            return rels;
        }

        protected List<DataLink> GetDataLinks(XElement doc, string type = "Data")
        {
            var data = (from l in doc.Elements("Link").Where(n => n.Attribute("Type").Value == type)
                        let q = l.Elements("Query")
                            .Elements("SQL")
                            .OrderByDescending(n => n.Attribute("Key")?.Value)
                        select new DataLink
                        {
                            Name = l.Attribute("Name").Value,
                            Type = type,
                            Source = l.Element("Source").Value,
                            Target = l.Element("Target").Value,
                            SQL = q.Select(n => string.Format("{0} => {1}{2}", 
                                n.Attribute("Source").Value, 
                                n.Attribute("Target").Value,
                                n.Attribute("Key")?.Value == "Y" ? " (K)" : "")).ToList()
                        })
                       .ToList();

            return data;
        }

        protected List<DataLink> GetSelectLinks(XElement doc)
        {
            return this.GetDataLinks(doc, "SelectList");
        }

        protected List<DataLink> GetDeleteLinks(XElement doc)
        {
            return this.GetDataLinks(doc, "Delete");
        }

        protected List<GroupLink> GetGroupLinks(XElement doc)
        {
            var rels = (from g in doc.Elements("Link").Where(n => n.Attribute("Type").Value == "Group")
                        from l in g.Elements("Import")
                        let seq = l.Attribute("Sequence").Value
                        select new GroupLink
                        {
                            GroupName = g.Attribute("Name").Value,
                            Name = l.Attribute("Name").Value,
                            Type = l.Attribute("Type").Value,
                            Sequence = string.IsNullOrEmpty(seq) ? 0 : Convert.ToInt32(seq),
                        })
                      .ToList();

            return rels;
        }

    }
}