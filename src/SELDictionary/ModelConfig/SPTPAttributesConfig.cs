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
    class SPTPAttributesConfig : IEntityTypeConfiguration<SPTPAttributes>
    {
        public void Configure(EntityTypeBuilder<SPTPAttributes> builder)
        {
            builder.ToTable(nameof(SPTPAttributes));

            builder
                .Property(e => e.SPTPLayoutsID)
                .HasColumnName("SPTPLAYOUTS_ID");

            builder
                .HasOne(e => e.ItemAttribute)
                .WithOne()
                .HasForeignKey<SPTPAttributes>(e => e.AttributeID);
        }
    }
}
