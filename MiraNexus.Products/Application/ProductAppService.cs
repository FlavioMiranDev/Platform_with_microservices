using MiraNexus.Products.Data.Mapper;
using MiraNexus.Products.Models;
using MiraNexus.Contracts.Protos;
using MiraNexus.Products.Repositories;

namespace MiraNexus.Products.Application;

public class ProductAppService
{
    private readonly ProductRepository _repository;

    public ProductAppService(ProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductResponse?> GetById(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product is null) return null;

        return product.ToProductResponse();
    }

    public async Task<ProductListResponse> GetAll(Guid tenantId)
    {
        var products = await _repository.GetAllAsync(tenantId);

        var response = new ProductListResponse();

        foreach (var product in products) response.Products.Add(product.ToProductResponse());

        return response;
    }

    public async Task<ProductResponse> Create(ProductCreateRequest request)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            TenantId = Guid.Parse(request.TenantId),
            CategoryId = Guid.Parse(request.CategoryId),
            Name = request.Name.Trim(),
            Description = request.Description,
            Price = (decimal)request.Price,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            Ref = request.Ref ?? null
        };

        var created = await _repository.CreateAsync(product);

        return created.ToProductResponse();
    }

    public async Task<ProductResponse?> Update(ProductUpdateRequest request)
    {
        var productId = Guid.Parse(request.Id);
        var tenantId = Guid.Parse(request.TenantId);
        var product = await _repository.GetByIdAsync(productId);

        if (product is null || product.TenantId != tenantId) return null;

        product.CategoryId = Guid.Parse(request.CategoryId);
        product.Name = request.Name.Trim();
        product.Description = request.Description;
        product.Price = (decimal)request.Price;
        product.IsActive = request.IsActive;
        product.UpdatedAt = DateTime.UtcNow;
        product.Ref = request.Ref ?? null;

        await _repository.UpdateAsync(product);

        return product.ToProductResponse();
    }

    public async Task Delete(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
