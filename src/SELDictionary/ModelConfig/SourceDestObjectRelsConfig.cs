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
    public class SourceDestObjectRelsConfig : IEntityTypeConfiguration<SourceDestObjectRels>
    {
        public void Configure(EntityTypeBuilder<SourceDestObjectRels> builder)
        {
            builder.ToTable(nameof(SourceDestObjectRels));

            builder.HasKey(e => e.ID);

            builder
                .HasOne(e => e.ParentItem)
                .WithOne()
                .HasForeignKey<SourceDestObjectRels>(e => e.ParentItemID)
                .IsRequired();

            builder
                .HasOne(e => e.ChildItem)
                .WithOne()
                .HasForeignKey<SourceDestObjectRels>(e => e.ChildItemID)
                .IsRequired();

        }
    }
}
