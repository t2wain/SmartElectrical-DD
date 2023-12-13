using SELCmdLets.Config;
using System.Management.Automation;
using System.Reflection;

namespace SELCmdLets
{
    [Cmdlet(VerbsCommon.New, "SELServiceProvider")]
    public class NewSELServiceProvider : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateNotNullOrEmpty]
        public string ConfigFile { get; set; }

        [Parameter(Position = 2)]
        [ValidateSet("SPEL", "SPAPLANT", "SPSITE")]
        public string AppName { get; set; }

        protected override void BeginProcessing()
        {
            // a work around to load the local Oracle.ManagedDataAccess.dll into PowerShell
            // instead of the GAC version which is incompatible with dotNET core.
            var fi = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var asmPath = Path.Join(fi.Directory!.FullName, "Oracle.ManagedDataAccess.dll");
            var script = $"Add-Type -AssemblyName \"{asmPath}\"";
            //Assembly.LoadFile(asmPath);
            ScriptBlock.Create(script).Invoke();

            if (!string.IsNullOrEmpty(AppName))
                SessionState.PSVariable.Set(Constants.APP_NAME, AppName);
        }
        protected override void ProcessRecord()
        {
            var provider = SessionState.PSVariable.Get(Constants.SERVICE_PROVIDER)?.Value as IServiceProvider;
            if (provider != null)
                (provider as IDisposable)?.Dispose();
            provider = ServiceCollectionExtensions.InitProvider(ConfigFile);
            SessionState.PSVariable.Set(Constants.SERVICE_PROVIDER, provider);
            WriteObject(provider);
        }
    }
}
