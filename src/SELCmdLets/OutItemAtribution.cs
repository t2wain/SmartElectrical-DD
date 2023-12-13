using Microsoft.Extensions.DependencyInjection;
using SELDictionary;
using System.Management.Automation;

namespace SELCmdLets
{
    [Cmdlet(VerbsData.Out, "SELItemAttribution")]
    public class OutItemAtribution : PSCmdlet
    {
        [Parameter()]
        public IServiceProvider ServiceProvider { get; set; }

        [Parameter(Position = 2, ValueFromPipelineByPropertyName = true)]
        [ValidateSet("SPEL", "SPAPLANT", "SPSITE")]
        public string AppName { get; set; }

        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public int ItemID { get; set; }

        [Parameter]
        public SwitchParameter WithSQL { get; set; }

        IServiceProvider _provider;
        string _appName;
        IRepository? _repo;
        ItemSQL? _itemSQL;
        StringWriter _writer;

        protected override void BeginProcessing()
        {
            _provider = ServiceProvider;
            if (_provider == null)
                _provider = SessionState.PSVariable.Get(Constants.SERVICE_PROVIDER).Value as IServiceProvider;

            _appName = AppName;
            if (string.IsNullOrEmpty(_appName))
                _appName = SessionState.PSVariable.Get(Constants.APP_NAME).Value.ToString();

            _itemSQL = _provider.GetService<ItemSQL>();
            _repo = Utility.GetRepository(_provider, _appName);
            _writer = new StringWriter();
        }

        protected override void ProcessRecord()
        {
            var sb = _writer.GetStringBuilder();
            sb.Remove(0, sb.Length);
            var item = _repo.FindItem(ItemID).Result;
            var res = _itemSQL.GetItemJoin(item, _repo).Result;
            if (WithSQL.ToBool())
                res.PrintAllSQL(_writer);
            else ItemJoin.PrintItemAttributions(item, _writer);
            WriteObject(_writer.ToString());
        }

        protected override void EndProcessing()
        {
            _writer.Dispose();
        }
    }
}
