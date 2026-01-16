namespace MiraNexus.Products.Models;

public class Category : EntityBase
{
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Navigation properties
    public virtual Tenant Tenant { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}