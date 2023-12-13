using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SELDictionary.Model;

namespace SELDictionary.ModelConfig
{
    public class EntitiesConfig : IEntityTypeConfiguration<Entities>
    {
        public void Configure(EntityTypeBuilder<Entities> builder)
        {
            builder.ToTable(nameof(Entities));

            builder
                .HasMany(e => e.EntityAttrs)
                .WithOne(e => e.Entity)
                .HasForeignKey(e => e.EntityNumber);

            builder
                .HasKey(e => e.EntityNumber);

            builder
                .HasMany(e => e.EntityAttrs);

            builder
                .Property(e => e.EntityNumber)
                .HasColumnName("ENTITY_NUMBER");

            builder
                .Property(e => e.EntityName)
                .HasColumnName("ENTITY_NAME");

            builder
                .Property(e => e.EntityDescription)
                .HasColumnName("ENTITY_DESCRIPTION");

            builder
                .Property(e => e.EntityApps)
                .HasColumnName("ENTITY_APPS");

            builder
                .Property(e => e.EntityIdenAtts)
                .HasColumnName("ENTITY_IDEN_ATTS");

        }
    }
}
