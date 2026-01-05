using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monetra.Domain.BackOffice.Entities;

namespace Monetra.Infra.Data.Mapping;

public class ExpenseMapping : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("Expense");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Description)
            .HasColumnType("TEXT")
            .IsRequired();
        
        builder.HasMany(x => x.RecurringTransactions)
            .WithOne(x => x.Expense)
            .HasForeignKey(x => x.ExpenseId);
        
        builder.HasOne(x => x.Portfolio)
            .WithOne(x => x.Exepense)
            .HasForeignKey<Portfolio>(x=>x.ExpenseId);
    }
}