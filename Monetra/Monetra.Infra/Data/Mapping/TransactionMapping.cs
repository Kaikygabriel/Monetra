using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monetra.Domain.BackOffice.Entities;

namespace Monetra.Infra.Data.Mapping;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Amount)
            .HasColumnType("MONEY")
            .IsRequired(true);
        builder.Property(x => x.CreatedAt)
            .HasColumnType("DATETIME")
            .IsRequired(true);
        builder.Property(x => x.Type)
            .IsRequired(true);
    }
}