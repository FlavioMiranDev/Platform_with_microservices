namespace MiraNexus.Products.Models;

public class Tenant : EntityBase
{
    public string CompanyName { get; set; }
    public string TradingName { get; set; }
    public string TaxId { get; set; } // CNPJ
    public string? StateRegistration { get; set; } // Inscrição Estadual
    public string? MunicipalRegistration { get; set; } // Inscrição Municipal
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Street { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    
    // Navigation properties
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}