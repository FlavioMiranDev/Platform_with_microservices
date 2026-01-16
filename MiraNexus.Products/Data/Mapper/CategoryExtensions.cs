using MiraNexus.Products.Protos;
using MiraNexus.Products.Data.Extentions;
using MiraNexus.Products.Models;

namespace MiraNexus.Products.Data.Mapper;

public static class CategoryExtensions
{
    public static CategoryResponse ToCategoryResponse(this Category category)
    {
        return new CategoryResponse
        {
            Id = category.Id.ToString(),
            TenantId = category.TenantId.ToString(),
            Name = category.Name,
            Description = category.Description,
            CreatedAt = category.CreatedAt.ToTimestamp(),
            UpdatedAt = category.UpdatedAt.ToTimestamp()
        };
    }
}
