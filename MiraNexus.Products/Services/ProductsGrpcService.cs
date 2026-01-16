using Grpc.Core;
using MiraNexus.Products.Application;
using MiraNexus.Products.Protos;

namespace MiraNexus.Products.Services;

public class ProductsGrpcService(ProductAppService appService) : ProductService.ProductServiceBase
{
    private readonly ProductAppService _appService = appService;

    public override async Task<ProductResponse> GetById(
        ProductRequestById request,
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out var productId)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid product id"));

        var product = await _appService.GetById(productId) ?? throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));

        return product;
    }

    public override async Task<ProductListResponse> GetAll(
        ProductListRequest request,
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.TenantId, out var tenantId)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid tenantId"));

        return await _appService.GetAll(tenantId);
    }

    public override async Task<ProductResponse> Create(
        ProductCreateRequest request,
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.TenantId, out _)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid tenantId"));

        if (!Guid.TryParse(request.CategoryId, out _)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid categoryId"));

        if (string.IsNullOrWhiteSpace(request.Name)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Product name is required"));

        return await _appService.Create(request);
    }

    public override async Task<ProductResponse> Update(
    ProductUpdateRequest request,
    ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out _)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid productId"));

        if (!Guid.TryParse(request.TenantId, out _)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid tenantId"));

        if (!Guid.TryParse(request.CategoryId, out _)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid categoryId"));

        if (string.IsNullOrWhiteSpace(request.Name)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Product name is required"));

        var result = await _appService.Update(request) ?? throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));

        return result;
    }

    public override async Task<ProductDeleteResponse> Delete(
    ProductDeleteRequest request,
    ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out Guid guid)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid product id"));

        await _appService.Delete(guid);

        return new ProductDeleteResponse();
    }
}
