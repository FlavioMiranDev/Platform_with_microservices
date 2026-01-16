using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiraNexus.Products.Models;

namespace MiraNexus.Products.Data.Configurations;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(p => p.TenantId)
            .HasColumnName("tenant_id")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(p => p.CategoryId)
            .HasColumnName("category_id")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasMaxLength(1500);

        builder.Property(p=>p.Ref)
            .HasColumnName("ref")
            .HasMaxLength(15);

        builder.Property(p => p.Price)
            .HasColumnName("price")
            .HasPrecision(10, 3)
            .HasDefaultValue(0.0)
            .IsRequired();

        builder.Property(p => p.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue<bool>(true);

        builder.Property(p => p.CreatedAt)
            .HasColumnName("createdAt")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.UpdatedAt)
            .HasColumnName("updatedAt")
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");


        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Tenant)
            .WithMany(t => t.Products)
            .HasForeignKey(p => p.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
