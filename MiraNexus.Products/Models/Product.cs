using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiraNexus.Products.Models;

[Table("products")]
public class Product
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [ForeignKey("Tenant")]
    [Column("tenant_id")]
    public Guid TenantId { get; set; }

    [Required]
    [ForeignKey("Category")]
    [Column("category_id")]
    public Guid CategoryId { get; set; }

    [Required]
    [MaxLength(150)]
    [Column("name")]
    public string Name { get; set; }

    [Column("description", TypeName = "text")]
    public string Description { get; set; }

    [Required]
    [Column("price", TypeName = "decimal(10,3)")]
    public decimal Price { get; set; }

    [Required]
    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    [ForeignKey("TenantId")]
    public virtual Tenant Tenant { get; set; }

    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }
}