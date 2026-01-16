using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiraNexus.Products.Models;

namespace MiraNexus.Products.Data.Configurations;

public class TenantMap : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("tenants");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(t => t.CompanyName)
            .HasColumnName("company_name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.TradingName)
            .HasColumnName("trading_name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.TaxId)
            .HasColumnName("tax_id")
            .HasMaxLength(18)
            .IsRequired();

        builder.Property(t => t.StateRegistration)
            .HasColumnName("state_registration")
            .HasMaxLength(20);

        builder.Property(t => t.MunicipalRegistration)
            .HasColumnName("municipal_registration")
            .HasMaxLength(100);

        builder.Property(t => t.Email)
            .HasColumnName("email")
            .HasMaxLength(200);

        builder.Property(t => t.Phone)
            .HasColumnName("phone")
            .HasMaxLength(20);

        builder.Property(t => t.Street)
            .HasColumnName("street")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.Neighborhood)
            .HasColumnName("neighborhood")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(t => t.City)
            .HasColumnName("city")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.State)
            .HasColumnName("state")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(t => t.PostalCode)
            .HasColumnName("postal_code")
            .HasMaxLength(9)
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .HasColumnName("createdAt")
            .ValueGeneratedOnAdd();

        builder.Property(t => t.UpdatedAt)
            .HasColumnName("updatedAt")
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");

    }
}
