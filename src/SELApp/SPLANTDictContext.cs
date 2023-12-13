using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SELDictionary;
using SELDictionary.ModelConfig;

namespace SELApp
{
    public class SPLANTDictContext : DictDbContext, ISPLANTDictContextReadOnly
    {
        public SPLANTDictContext(DbContextOptions<SPLANTDictContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) { }
        }
    }
}
