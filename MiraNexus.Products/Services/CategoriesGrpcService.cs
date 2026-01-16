using Grpc.Core;
using MiraNexus.Products.Protos;
using MiraNexus.Products.Application;

namespace MiraNexus.Products.Services;

public class CategoriesGrpcService : CategoryService.CategoryServiceBase
{
    private readonly CategoryAppService _appService;

    public CategoriesGrpcService(CategoryAppService appService)
    {
        _appService = appService;
    }

    public override async Task<CategoryResponse> GetById(CategoryRequestById request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out var productId)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid product id"));

        var category = await _appService.GetByIdAsync(productId) ?? throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));

        return category;
    }

    public override async Task<CategoryListResponse> GetAll(CategoryListRequest request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.TenantId, out var tenantId)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid tenantId"));

        return await _appService.GetAllAsync(tenantId);
    }

    public override async Task<CategoryResponse> Create(CategoryCreateRequest request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.TenantId, out _)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid tenantId"));

        if (string.IsNullOrWhiteSpace(request.Name)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Category name is required"));

        return await _appService.CreateAsync(request);
    }

    public override async Task<CategoryResponse> Update(CategoryUpdateRequest request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out _)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid categoryId"));

        if (!Guid.TryParse(request.TenantId, out _)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid tenantId"));

        if (string.IsNullOrWhiteSpace(request.Name)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Category name is required"));

        if (string.IsNullOrWhiteSpace(request.Description)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Category description is required"));

        var result = await _appService.UpdateAsync(request) ?? throw new RpcException(new Status(StatusCode.NotFound, "Category not found"));

        return result;
    }

    public override async Task<CategoryDeleteResponse> Delete(CategoryDeleteRequest request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out Guid guid)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid category id"));

        await _appService.Delete(guid);

        return new CategoryDeleteResponse();
    }
}
