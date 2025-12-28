using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monetra.Domain.BackOffice.Entities;

namespace Monetra.Infra.Data.Mapping;

public class PortfolioMapping : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.HasKey(x => x.Id)
            .HasName("Pk_Portofolio_Id");
        builder.Property(x => x.CreateDate)
            .IsRequired(true)
            .HasColumnType("DATETIME");
        builder.Property(x => x.Title)
            .IsRequired(true)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120);
        builder.Property(x => x.Visible)
            .IsRequired(true)
            .HasColumnType("BIT");
        builder.OwnsOne(x => x.FixedIncome, x =>
        {
            x.Property(x => x.Value)
                .HasColumnType("MONEY")
                .IsRequired(true);
        });
        builder.OwnsOne(x => x.VariableIncome, x =>
        {
            x.Property(x => x.Value)
                .HasColumnType("MONEY")
                .IsRequired(true);
        });
        
    }
}