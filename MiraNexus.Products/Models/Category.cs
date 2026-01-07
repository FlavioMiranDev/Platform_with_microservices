using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiraNexus.Products.Models;

[Table("categories")]
public class Category
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [ForeignKey("Tenant")]
    [Column("tenant_id")]
    public Guid TenantId { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; }

    [Column("description", TypeName = "text")]
    public string Description { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    [ForeignKey("TenantId")]
    public virtual Tenant Tenant { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}