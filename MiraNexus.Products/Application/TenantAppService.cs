using MiraNexus.Products.Data.Mapper;
using MiraNexus.Products.Models;
using MiraNexus.Contracts.Protos;
using MiraNexus.Products.Repositories;

namespace MiraNexus.Products.Application;

public class TenantAppService(TenantRepository repository)
{
    private readonly TenantRepository _repository = repository;

    public async Task<TenantResponse?> GetByIdAsync(Guid id)
    {
        var tenant = await _repository.GetByIdAsync(id);

        if (tenant is null) return null;

        return tenant.ToTenantResponse();
    }

    public async Task<TenantResponse> CreateAsync(TenantCreateRequest request)
    {
        var tenant = new Tenant
        {
            Id = Guid.NewGuid(),
            CompanyName = request.CompanyName,
            TradingName = request.TradingName,
            TaxId = request.TaxId,
            StateRegistration = request.StateRegistration ?? null,
            MunicipalRegistration = request.MunicipalRegistration ?? null,
            Email = request.Email ?? null,
            Phone = request.Phone ?? null,
            Street = request.Street,
            Neighborhood = request.Neighborhood,
            City = request.City,
            State = request.State,
            PostalCode = request.PostalCode
        };

        var created = await _repository.CreateAsync(tenant);

        return created.ToTenantResponse();
    }

    public async Task<TenantResponse?> UpdateAsync(TenantUpdateRequest request)
    {
        var tenantId = Guid.Parse(request.Id);
        var tenant = await _repository.GetByIdAsync(tenantId);

        if (tenant is null) return null;

        tenant.CompanyName = request.CompanyName;
        tenant.TradingName = request.TradingName;
        tenant.TaxId = request.TaxId;
        tenant.StateRegistration = request.StateRegistration;
        tenant.MunicipalRegistration = request.MunicipalRegistration;
        tenant.Email = request.Email;
        tenant.Phone = request.Phone;
        tenant.Street = request.Street;
        tenant.Neighborhood = request.Neighborhood;
        tenant.City = request.City;
        tenant.State = request.State;
        tenant.PostalCode = request.PostalCode;

        await _repository.UpdateAsync(tenant);

        return tenant.ToTenantResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
