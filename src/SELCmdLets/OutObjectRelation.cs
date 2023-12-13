using Microsoft.Extensions.DependencyInjection;
using SELDictionary;
using System.Management.Automation;

namespace SELCmdLets
{
    [Cmdlet(VerbsData.Out, "SELObjectRelation")]
    public class OutObjectRelation : PSCmdlet
    {
        [Parameter()]
        public IServiceProvider ServiceProvider { get; set; }

        [Parameter(Position = 2, ValueFromPipelineByPropertyName = true)]
        [ValidateSet("SPEL", "SPAPLANT", "SPSITE")]
        public string AppName { get; set; }

        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true, 
            ValueFromPipelineByPropertyName = true)]
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
            if (WithSQL.ToBool())
            {
                var res = _itemSQL.GetItemObjRelJoin(ItemID, _repo);
                if (res != null)
                    res.PrintAllSQL(_writer);

            }
            else ObjectJoin.PrintRelations(new int[] { ItemID }, _repo, _writer);
            WriteObject(sb.ToString());
        }

        protected override void EndProcessing()
        {
            _writer.Dispose();
            _writer = null;
        }
    }
}
