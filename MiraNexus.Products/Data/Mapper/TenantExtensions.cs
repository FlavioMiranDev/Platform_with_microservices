using MiraNexus.Products.Protos;
using MiraNexus.Products.Models;
using MiraNexus.Products.Data.Extentions;

namespace MiraNexus.Products.Data.Mapper;

public static class TenantExtensions
{
    public static TenantResponse ToTenantResponse(this Tenant tenant)
    {
        return new TenantResponse
        {
            Id = tenant.Id.ToString(),
            CompanyName = tenant.CompanyName,
            TradingName = tenant.TradingName,
            TaxId = tenant.TaxId,
            Street = tenant.Street,
            Neighborhood = tenant.Neighborhood,
            City = tenant.City,
            State = tenant.State,
            PostalCode = tenant.PostalCode,
            StateRegistration = tenant.StateRegistration ?? null,
            MunicipalRegistration = tenant.MunicipalRegistration ?? null,
            Email = tenant.Email ?? null,
            Phone = tenant.Phone ?? null,
            CreatedAt = tenant.CreatedAt.ToTimestamp(),
            UpdatedAt = tenant.CreatedAt.ToTimestamp()
        };
    }
}
