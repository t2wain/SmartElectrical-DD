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
    public class CorrelAttsConfig : IEntityTypeConfiguration<CorrelAtts>
    {
        public void Configure(EntityTypeBuilder<CorrelAtts> builder)
        {
            builder.ToTable(nameof(CorrelAtts));

            builder
                .HasKey(e => e.CorrelAttNumber);

            builder
                .Property(e => e.CorrelAttNumber)
                .HasColumnName("CORRELATT_NUMBER");

            builder
                .Property(e => e.NumberOfAtts)
                .HasColumnName("NUMBER_OF_ATTS");
        }
    }
}
