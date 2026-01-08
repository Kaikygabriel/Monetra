using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monetra.Domain.BackOffice.Entities;
using Org.BouncyCastle.Tls;

namespace Monetra.Infra.Data.Mapping;

public class CustomerMapping : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");
        
        builder.HasKey(x => x.Id)
            .HasName("Pk_customer_id");
        
        builder.Property(x => x.Name)
            .HasMaxLength(120)
            .HasColumnType("VARCHAR")
            .IsRequired();

        builder.Property(x => x.Salary)
            .HasDefaultValue(0)
            .HasColumnType("MONEY");
        
        builder.Property(x => x.FinancialHealth)
            .HasConversion(x => x.Percentage, x => new FinancialHealth(x))
            .HasColumnName("FinancialHealth")
            .HasColumnType("TINYINT") 
            .IsRequired(true);
        
        builder.HasOne(x => x.User) ;
        
        builder.HasMany(x => x.Portfolios)
            .WithOne(x=>x.Customer)
            .HasForeignKey(x=>x.CustomerId)
            .HasConstraintName("Fk_Portfolio_Customer");
        
        builder.HasOne(x => x.Mark)
            .WithOne(x=>x.Customer)
            .HasForeignKey<Mark>(x=>x.CustomerId)
            .HasConstraintName("Fk_Mark_Customer");
        
        builder.HasOne(x => x.Expense)
            .WithOne(x => x.Customer)
            .HasForeignKey<Expense>(x=>x.CustomerId)
            .HasConstraintName("Fk_Expense_Customer");;
    }
}