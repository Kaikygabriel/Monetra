using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monetra.Domain.BackOffice.Entities;

namespace Monetra.Infra.Data.Mapping;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(x => x.Id)
            .HasName("Pk_User_Id");
        builder.Property(x => x.Password)
            .HasMaxLength(200)
            .HasColumnType("NVARCHAR")
            .IsRequired(true);
        builder.OwnsOne(x => x.Email ,b =>
        {
            b.Property(x => x.Address)
                .HasMaxLength(170)
                .HasColumnType("VARCHAR")
                .IsRequired(true);
           b.HasIndex(x => x.Address)
               .IsUnique()
               .HasDatabaseName("UX_User_Email"); 
        });
    }
}