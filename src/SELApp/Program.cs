using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SELApp;
using SELDictionary;

public class Program
{
    static void Main(string[] args)
    {
        var provider = InitProvider();
        var t = new DictTest(provider);
        t.Run();
    }

    public static IServiceProvider InitProvider()
    {
        IServiceCollection services = new ServiceCollection();
        var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");

        if (File.Exists("appSettings.Development.json"))
            builder.AddJsonFile("appSettings.Development.json");

        IConfiguration root = builder.Build();
        services.AddSingleton(root);
        services.ConfigureMyApp(root);
        IServiceProvider provider = services.BuildServiceProvider();
        return provider;
    }

}