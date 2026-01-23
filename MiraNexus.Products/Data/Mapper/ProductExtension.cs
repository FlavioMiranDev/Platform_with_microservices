using MiraNexus.Products.Data.Extentions;
using MiraNexus.Products.Models;
using MiraNexus.Contracts.Protos;

namespace MiraNexus.Products.Data.Mapper;

public static class ProductExtension
{
    public static ProductResponse ToProductResponse(this Product product)
    {
        return new ProductResponse
        {
            Id = product.Id.ToString(),
            TenantId = product.TenantId.ToString(),
            CategoryId = product.CategoryId.ToString(),
            Name = product.Name,
            Description = product.Description,
            Price = (float)product.Price,
            IsActive = product.IsActive,
            CreatedAt = product.CreatedAt.ToTimestamp(),
            UpdatedAt = product.UpdatedAt.ToTimestamp()
        };
    }
}
