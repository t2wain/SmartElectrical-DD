using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SELDictionary;
using SELDictionary.ModelConfig;

namespace SELCmdLets.Config
{
    public class SELDictContext : DictDbContext, ISELDictContextReadOnly
    {
        public SELDictContext(DbContextOptions<SELDictContext> options) : base(options)  { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) { }
        }

    }
}
