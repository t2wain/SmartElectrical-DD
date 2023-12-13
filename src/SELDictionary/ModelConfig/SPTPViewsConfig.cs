using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SELDictionary.Model;

namespace SELDictionary.ModelConfig
{
    public class SPTPViewsConfig : IEntityTypeConfiguration<SPTPViews>
    {
        public void Configure(EntityTypeBuilder<SPTPViews> builder)
        {
            builder.ToTable(nameof(SPTPViews));

            builder
                .Property(e => e.AppID)
                .HasColumnName("APP_ID");

            builder
                .HasOne(e => e.Item)
                .WithOne()
                .HasForeignKey<SPTPViews>(e => e.ItemType);

            builder
                .HasOne(e => e.ParentView)
                .WithOne()
                .HasForeignKey<SPTPViews>(e => e.ParentID);

            builder
                .HasMany(e => e.ViewLayouts)
                .WithOne()
                .HasForeignKey(e => e.SPTPViewsID);

            builder
                .HasOne(e => e.DefaultViewLayout)
                .WithOne()
                .HasForeignKey<SPTPViews>(e => e.DefaultLayout);
        }
    }
}
