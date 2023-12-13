using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SELDictionary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.ModelConfig
{
    public class UniqueAttsConfig : IEntityTypeConfiguration<UniqueAtts>
    {
        public void Configure(EntityTypeBuilder<UniqueAtts> builder)
        {
            builder.ToTable(nameof(UniqueAtts));

            builder
                .HasKey(e => e.ID);

            //builder
            //    .HasOne<Entities>()
            //    .WithMany(e => e.EntityAttrs)
            //    .HasForeignKey(e => e.EntityNumber)
            //    .IsRequired();

            builder
                .HasOne(e => e.Attribute)
                .WithOne()
                .HasForeignKey<UniqueAtts>(e => e.AttributeNumber)
                .IsRequired();

            builder
                .HasOne(e => e.Entity)
                .WithMany(e => e.EntityAttrs)
                .HasForeignKey(e => e.EntityNumber)
                .IsRequired();

            builder
                .Property(e => e.EntityNumber)
                .HasColumnName("ENTITY_NUMBER");

            builder
                .Property(e => e.AttributeNumber)
                .HasColumnName("ATTRIBUTE_NUMBER");

            builder
                .Property(e => e.UniqueName)
                .HasColumnName("UNIQUE_NAME");

            builder
                .Property(e => e.DisplayName)
                .HasColumnName("DISPLAY_NAME");

        }
    }
}
