using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SELDictionary;
using SELDictionary.ModelConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELCmdLets.Config
{
    public class SITEDDictContext : DictDbContext, ISITEDictContextReadOnly
    {
        public SITEDDictContext(DbContextOptions<SITEDDictContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) { }
        }

    }
}
