using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SELDictionary;
using SELDictionary.ModelConfig;

namespace SELCmdLets.Config
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider InitProvider(string configFile)
        {
            IServiceCollection services = new ServiceCollection();
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(configFile);
            IConfiguration root = builder.Build();
            services.AddSingleton(root);
            services.ConfigureMyApp(root);
            IServiceProvider provider = services.BuildServiceProvider();
            return provider;
        }

        public static IServiceCollection ConfigureMyApp(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<SELDictContext>(options =>
            {
                options.UseOracle(config.GetConnectionString("selDict"))
                    //.AddInterceptors(new ConnectionInterceptor())
                    // to remove double-quote from generated SQL
                    .AddInterceptors(new QueryCommandInterceptor());
            });
            services.AddDbContext<SPLANTDictContext>(options =>
            {
                options.UseOracle(config.GetConnectionString("splantDict"))
                    //.AddInterceptors(new ConnectionInterceptor())
                    // to remove double-quote from generated SQL
                    .AddInterceptors(new QueryCommandInterceptor());
            });
            services.AddDbContext<SITEDDictContext>(options =>
            {
                options.UseOracle(config.GetConnectionString("ssiteDict"))
                    //.AddInterceptors(new ConnectionInterceptor())
                    // to remove double-quote from generated SQL
                    .AddInterceptors(new QueryCommandInterceptor());
            }); 
            services.AddTransient<ISPELDRepository, Repository>(p => 
                new Repository(p.GetRequiredService<SELDictContext>(), "SPEL"));
            services.AddTransient<IPLANTDRepository, Repository>(p =>
                new Repository(p.GetRequiredService<SPLANTDictContext>(), "SPAPLANT"));
            services.AddTransient<ISITEDRepository, Repository>(p =>
                new Repository(p.GetRequiredService<SITEDDictContext>(), "SPSITE"));
            services.AddTransient<ItemSQL>();
            return services;
        }
    }
}
