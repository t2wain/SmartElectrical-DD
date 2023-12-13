using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SELDictionary.Model;

namespace SELDictionary.ModelConfig
{
    public class EnumerationsConfig : IEntityTypeConfiguration<Enumerations>
    {
        public void Configure(EntityTypeBuilder<Enumerations> builder)
        {
            builder.ToTable(nameof(Enumerations));

            builder.HasKey(e => e.ID);

            builder
                .HasMany(e => e.CodeItems)
                .WithOne()
                .HasForeignKey(e => e.CodeListNumber);
        }
    }
}
