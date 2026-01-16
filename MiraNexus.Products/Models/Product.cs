namespace MiraNexus.Products.Models;

public class Product : EntityBase
{
    public Guid TenantId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string? Ref { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual Tenant Tenant { get; set; }
    public virtual Category Category { get; set; }
}