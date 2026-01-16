using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiraNexus.Products.Models;

namespace MiraNexus.Products.Data.Configurations;

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(c => c.TenantId)
            .HasColumnName("tenant_id")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(c => c.Name)
            .HasColumnName("name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasColumnName("description")
            .HasMaxLength(1500);

        builder.Property(c => c.CreatedAt)
            .HasColumnName("createdAt")
            .ValueGeneratedOnAdd();

        builder.Property(c => c.UpdatedAt)
            .HasColumnName("updatedAt")
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");


        builder.HasOne(c => c.Tenant)
            .WithMany(t => t.Categories)
            .HasForeignKey(c => c.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
