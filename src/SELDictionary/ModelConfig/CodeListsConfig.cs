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
    public class CodeListsConfig : IEntityTypeConfiguration<CodeLists>
    {
        public void Configure(EntityTypeBuilder<CodeLists> builder)
        {
            builder.ToTable(nameof(CodeLists));

            builder.HasKey(e => new { e.CodeListNumber, e.CodeListIndex });

            builder
                .Property(e => e.CodeListNumber)
                .HasColumnName("CODELIST_NUMBER");

            builder
                .Property(e => e.CodeListIndex)
                .HasColumnName("CODELIST_INDEX");

            builder
                .Property(e => e.CodeListText)
                .HasColumnName("CODELIST_TEXT");

            builder
                .Property(e => e.CodeListShortText)
                .HasColumnName("CODELIST_SHORT_TEXT");

            builder
                .Property(e => e.CodeListConstraint)
                .HasColumnName("CODELIST_CONSTRAINT");

            builder
                .Property(e => e.CodeListSortValue)
                .HasColumnName("CODELIST_SORT_VALUE");

            builder
                .Property(e => e.CodeListEntryDisabled)
                .HasColumnName("CODELIST_ENTRY_DISABLED");
        }
    }
}
