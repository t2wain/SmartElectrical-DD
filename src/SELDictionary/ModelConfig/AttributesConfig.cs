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
    public class AttributesConfig : IEntityTypeConfiguration<Attributes>
    {
        public void Configure(EntityTypeBuilder<Attributes> builder)
        {
            builder.ToTable(nameof(Attributes));

            builder
                .HasKey(e => e.AttributeNumber);

            builder
                .Property(e => e.AttributeNumber)
                .HasColumnName("ATTRIBUTE_NUMBER");

            builder
                .Property(e => e.AttributeName)
                .HasColumnName("ATTRIBUTE_NAME");

            builder
                .Property(e => e.AttributeDataType)
                .HasColumnName("ATTRIBUTE_DATATYPE");

            builder
                .Property(e => e.AttributeDefValue)
                .HasColumnName("ATTRIBUTE_DEFVALUE");

            builder
                .Property(e => e.AttributeUOM)
                .HasColumnName("ATTRIBUTE_UOM");

            builder
                .Property(e => e.AttributeDescription)
                .HasColumnName("ATTRIBUTE_DESCRIPTION");

            builder
                .Property(e => e.AttributeFormat)
                .HasColumnName("ATTRIBUTE_FORMAT");

            builder
                .Property(e => e.AttributeCodeListed)
                .HasColumnName("ATTRIBUTE_CODELISTED");

            builder
                .Property(e => e.AttributeDisplay)
                .HasColumnName("ATTRIBUTE_DISPLAY");

            builder
                .Property(e => e.AttributeUOMID)
                .HasColumnName("ATTRIBUTE_UOMID");
        }

    }
}
