using Microsoft.Extensions.DependencyInjection;
using SELDictionary;

namespace SELCmdLets
{
    internal static class Utility
    {
        public static IRepository? GetRepository(IServiceProvider ServiceProvider, string appName)
        {
            switch (appName)
            {
                case "SPEL":
                    return ServiceProvider.GetService<ISPELDRepository>();
                case "SPAPLANT":
                    return ServiceProvider.GetService<IPLANTDRepository>();
                case "SPSITE":
                    return ServiceProvider.GetService<ISITEDRepository>();
                default:
                    return null;
            };
        }
    }
}
