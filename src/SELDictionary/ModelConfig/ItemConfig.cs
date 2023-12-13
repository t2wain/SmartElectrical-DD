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
    public class ItemConfig : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable(nameof(Item));

            builder.HasKey(e => e.ID);

            builder
                .HasOne(e => e.SourceEntity)
                .WithOne()
                .HasForeignKey<Item>(e => e.SourceTable)
                .IsRequired();

            builder
                .HasMany(e => e.ItemAtts)
                .WithOne()
                .HasForeignKey(e => e.ItemID);
        }
    }
}
