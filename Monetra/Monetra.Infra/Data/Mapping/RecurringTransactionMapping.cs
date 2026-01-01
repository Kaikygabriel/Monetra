using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monetra.Domain.BackOffice.Entities;

namespace Monetra.Infra.Data.Mapping;

public class RecurringTransactionMapping : IEntityTypeConfiguration<RecurringTransaction>
{
    public void Configure(EntityTypeBuilder<RecurringTransaction> builder)
    {
        builder.ToTable("RecurringTransaction");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.MonthDayPayment)
            .HasColumnType("TINYINT")
            .IsRequired(true);
        
        builder.Property(x=>x.Value)
            .HasColumnType("MONEY")
            .IsRequired(true);
        
        builder.Property(x=>x.CostName)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired(true);

        builder.HasIndex(x => x.MonthDayPayment);
        builder
            .HasOne(x => x.Portfolio);
    }
}