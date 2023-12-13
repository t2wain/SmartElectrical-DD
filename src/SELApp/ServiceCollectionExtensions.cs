using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SELDictionary;
using SELDictionary.ModelConfig;

namespace SELApp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureMyApp(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<SELDictContext>(options =>
                options.UseOracle(config.GetConnectionString("selDict"))
                    //.AddInterceptors(new ConnectionInterceptor())
                    // to remove double-quote from generated SQL
                    .AddInterceptors(new QueryCommandInterceptor())
            );
            services.AddDbContext<SPLANTDictContext>(options =>
                options.UseOracle(config.GetConnectionString("splantDict"))
                    //.AddInterceptors(new ConnectionInterceptor())
                    // to remove double-quote from generated SQL
                    .AddInterceptors(new QueryCommandInterceptor())
            );
            services.AddDbContext<SITEDDictContext>(options =>
                options.UseOracle(config.GetConnectionString("ssiteDict"))
                    //.AddInterceptors(new ConnectionInterceptor())
                    // to remove double-quote from generated SQL
                    .AddInterceptors(new QueryCommandInterceptor())
            );
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
