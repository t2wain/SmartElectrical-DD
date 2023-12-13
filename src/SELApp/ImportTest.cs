using SELImportManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELApp
{
    public class ImportTest
    {
        public void Run()
        {
            //GetLinks();
            GetLinks(true);
        }

        protected void GetLinks(bool toFile = false) 
        {

            var files = new string[]
            {
                "Demo.xml",
                "Ref_Cable_ImportLink.Xml",
                "SPI_Import.Xml",
                "ReferenceCableWayComponents.xml",
                "H443ImportLinks.xml",
                "K859ImportData.xml",
                "P4ImportData.xml",
                "P6ImportData.xml"
            };

            var p = new ProjectXML();
            foreach (var fn in files ) 
            {
                var links = p.GetLinks(@".\Data\" + fn);
                if (toFile)
                {
                    var fi = new FileInfo(fn);
                    var fileName = fi.Name + ".txt";
                    using (StreamWriter writer = new StreamWriter(fileName)) 
                    {
                        links.Print(writer); 
                    }
                    Console.WriteLine(fileName);
                }
                else links.Print();
            }
        }
    }
}
