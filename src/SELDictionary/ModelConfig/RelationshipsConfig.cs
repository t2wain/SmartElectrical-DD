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
    public class RelationshipsConfig : IEntityTypeConfiguration<Relationships>
    {
        public void Configure(EntityTypeBuilder<Relationships> builder)
        {
            builder.ToTable(nameof(Relationships));

            builder
                .HasOne(e => e.SourceEntityObj)
                .WithOne()
                .HasForeignKey<Relationships>(e => e.SourceEntity);

            builder
                .HasOne(e => e.DestEntityObj)
                .WithOne()
                .HasForeignKey<Relationships>(e => e.DestEntity);

            builder
                .HasOne(e => e.SourceCorAttObj)
                .WithOne()
                .HasForeignKey<Relationships>(e => e.SourceCorAtt);

            builder
                .HasOne(e => e.DestCorAttObj)
                .WithOne()
                .HasForeignKey<Relationships>(e => e.DestCorAtt);

            builder
                .HasKey(e => e.RelNumber);

            builder
                .Property(e => e.RelNumber)
                .HasColumnName("REL_NUMBER");

            builder
                .Property(e => e.RelType)
                .HasColumnName("REL_TYPE");

            builder
                .Property(e => e.SourceEntity)
                .HasColumnName("SOURCE_ENTITY");

            builder
                .Property(e => e.SourceCorAtt)
                .HasColumnName("SOURCE_CORATT");

            builder
                .Property(e => e.DestEntity)
                .HasColumnName("DEST_ENTITY");

            builder
                .Property(e => e.DestCorAtt)
                .HasColumnName("DEST_CORATT");

            builder
                .Property(e => e.PositionAtt)
                .HasColumnName("POSITION_ATT");

            builder
                .Property(e => e.RelNumber)
                .HasColumnName("REL_NUMBER");

            builder
                .Property(e => e.TypeAttNumber)
                .HasColumnName("TYPE_ATT_NUMBER");

            builder
                .Property(e => e.TypeValue)
                .HasColumnName("TYPE_VALUE");

            builder
                .Property(e => e.OneToOne)
                .HasColumnName("ONE_TO_ONE");

            builder
                .Property(e => e.SourceSchema)
                .HasColumnName("SOURCE_SCHEMA");

            builder
                .Property(e => e.DestSchema)
                .HasColumnName("DEST_SCHEMA");

        }
    }
}
