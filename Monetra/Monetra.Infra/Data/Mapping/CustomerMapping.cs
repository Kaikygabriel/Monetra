using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monetra.Domain.BackOffice.Entities;

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
    
        builder.HasOne(x => x.User);
        builder.HasMany(x => x.Portfolios)
            .WithOne(x=>x.Customer);
        builder.HasOne(x => x.Mark);
    }
}