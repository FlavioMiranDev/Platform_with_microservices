
using MiraNexus.Products.Protos;
using MiraNexus.Products.Data.Mapper;
using MiraNexus.Products.Models;
using MiraNexus.Products.Repositories;

namespace MiraNexus.Products.Application;

public class CategoryAppService
{
    private readonly CategoryRepository _repository;

    public CategoryAppService(CategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<CategoryResponse?> GetByIdAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category is null) return null;

        return category.ToCategoryResponse();
    }

    public async Task<CategoryListResponse> GetAllAsync(Guid tenantId)
    {
        var categories = await _repository.GetAllAsync(tenantId);

        var response = new CategoryListResponse();

        foreach (var category in categories) response.Categories.Add(category.ToCategoryResponse());

        return response;
    }

    public async Task<CategoryResponse> CreateAsync(CategoryCreateRequest request)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            TenantId = Guid.Parse(request.TenantId),
            Name = request.Name,
            Description = request.Description
        };

        var created = await _repository.CreateAsync(category);

        return created.ToCategoryResponse();
    }

    public async Task<CategoryResponse?> UpdateAsync(CategoryUpdateRequest request)
    {
        var tenantId = Guid.Parse(request.TenantId);
        var categoryId = Guid.Parse(request.Id);
        var category = await _repository.GetByIdAsync(categoryId);

        if (category is null || category.TenantId != tenantId) return null;

        category.Name = request.Name.Trim();
        category.Description = request.Description.Trim();

        await _repository.UpdateAsync(category);

        return category.ToCategoryResponse();
    }

    public async Task Delete(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
