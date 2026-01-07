using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiraNexus.Products.Models;

[Table("tenants")]
public class Tenant
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(200)]
    [Column("company_name")]
    public string CompanyName { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("trading_name")]
    public string TradingName { get; set; }

    [Required]
    [MaxLength(18)]
    [Column("tax_id")]
    public string TaxId { get; set; } // CNPJ

    [MaxLength(20)]
    [Column("state_registration")]
    public string? StateRegistration { get; set; } // Inscrição Estadual

    [MaxLength(20)]
    [Column("municipal_registration")]
    public string? MunicipalRegistration { get; set; } // Inscrição Municipal

    [EmailAddress]
    [MaxLength(100)]
    [Column("email")]
    public string? Email { get; set; }

    [MaxLength(20)]
    [Column("phone")]
    public string? Phone { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("street")]
    public string Street { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("neighborhood")]
    public string Neighborhood { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("city")]
    public string City { get; set; }

    [Required]
    [MaxLength(2)]
    [Column("state")]
    public string State { get; set; }

    [Required]
    [MaxLength(9)]
    [Column("postal_code")]
    public string PostalCode { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}