using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiraNexus.Auth.Models;

namespace MiraNexus.Auth.Data.Maps;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.Property(u => u.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(u => u.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(u => u.Password)
            .HasColumnName("password")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(u=>u.Doc)
            .HasColumnName("cpf_cpnj")
            .HasMaxLength(14)
            .IsRequired();

        builder.Property(u => u.Phone)
            .HasColumnName("phone")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(u => u.IsActive)
            .HasColumnName("active");

        builder.Property(u=>u.ValidationCode)
            .HasColumnName("validation_code")
            .HasMaxLength(6);

            
        builder.Property(e => e.CreatedAt)
            .HasColumnName("createdAt")
            .ValueGeneratedOnAdd();
    }
}
