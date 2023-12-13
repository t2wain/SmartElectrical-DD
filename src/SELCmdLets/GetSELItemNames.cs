using SELDictionary;
using System.Management.Automation;

namespace SELCmdLets
{
    [Cmdlet(VerbsCommon.Get, "SELItemNames")]
    public class GetSELItemNames : PSCmdlet
    {
        [Parameter()]
        public IServiceProvider ServiceProvider { get; set; }

        [Parameter(Position = 1)]
        [ValidateSet("SPEL", "SPAPLANT", "SPSITE")]
        public string AppName { get; set; }


        IRepository? _repo;
        string _appName;
        IServiceProvider _provider;

        protected override void BeginProcessing()
        {
            _provider = ServiceProvider;
            if (_provider == null)
                _provider = SessionState.PSVariable.Get(Constants.SERVICE_PROVIDER).Value as IServiceProvider;

            _appName = AppName;
            if (string.IsNullOrEmpty(_appName))
                _appName = SessionState.PSVariable.Get(Constants.APP_NAME).Value.ToString();

            _repo = Utility.GetRepository(_provider, _appName);
        }

        protected override void ProcessRecord()
        {
            var itemNames = _repo!.GetAllItemNames().Result;
            WriteObject(itemNames, true);
        }
    }
}
