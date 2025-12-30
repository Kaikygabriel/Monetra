using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monetra.Domain.BackOffice.Entities;

namespace Monetra.Infra.Data.Mapping;

public class MarkMapping : IEntityTypeConfiguration<Mark>
{
    public void Configure(EntityTypeBuilder<Mark> builder)
    {
        builder.ToTable("Mark");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(120)
            .HasColumnType("NVARCHAR")
            .IsRequired(true);
        
        builder.Property(x => x.CreateAt)
            .HasColumnType("DATETIME")
            .IsRequired(true);
        
        builder.Property(x => x.TargetAmount)
            .HasPrecision(10,2)
            .HasColumnType("MONEY")
            .IsRequired(true);
        
        builder.Property(x => x.Deadline)
            .HasColumnType("DATETIME")
            .IsRequired(true);
    }
}