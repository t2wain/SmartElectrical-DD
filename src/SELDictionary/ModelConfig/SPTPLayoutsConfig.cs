using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SELDictionary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.ModelConfig
{
    public class SPTPLayoutsConfig : IEntityTypeConfiguration<SPTPLayouts>
    {
        public void Configure(EntityTypeBuilder<SPTPLayouts> builder)
        {
            builder.ToTable(nameof(SPTPLayouts));

            builder
                .Property(e => e.SPTPViewsID)
                .HasColumnName("SPTPVIEWS_ID");

            builder
                .HasMany(e => e.ViewAttributes)
                .WithOne()
                .HasForeignKey(e => e.SPTPLayoutsID);
        }
    }
}
