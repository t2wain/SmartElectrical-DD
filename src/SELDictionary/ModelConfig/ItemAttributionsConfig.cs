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
    public class ItemAttributionsConfig : IEntityTypeConfiguration<ItemAttributions>
    {
        public void Configure(EntityTypeBuilder<ItemAttributions> builder)
        {
            builder.ToTable(nameof(ItemAttributions));

            builder.HasKey(e => e.ID);

            builder
                .HasOne(e => e.Attribution)
                .WithOne()
                .HasForeignKey<ItemAttributions>(e => e.AttributionID);
        }
    }
}
