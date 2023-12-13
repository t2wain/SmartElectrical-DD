using SELImportManager;
using System.Management.Automation;

namespace SELCmdLets
{
    [Cmdlet(VerbsData.Out, "SELImportProject")]

    public class OutImportProject : Cmdlet
    {
        [Parameter(Position = 1, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public string FileName { get; set; }

        StringWriter _writer;

        protected override void BeginProcessing()
        {
            _writer = new StringWriter();
        }

        protected override void ProcessRecord()
        {
            var sb = _writer.GetStringBuilder();
            sb.Remove(0, sb.Length);
            var p = new ProjectXML();

            var links = p.GetLinks(FileName);
            links.Print(_writer);
            WriteObject(_writer.ToString());
        }
        protected override void EndProcessing()
        {
            _writer.Dispose();
            _writer = null;
        }

    }
}
