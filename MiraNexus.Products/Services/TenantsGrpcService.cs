using System;
using Grpc.Core;
using MiraNexus.Products.Application;
using MiraNexus.Products.Protos;

namespace MiraNexus.Products.Services;

public class TenantsGrpcService(TenantAppService appService) : TenantService.TenantServiceBase
{
    private readonly TenantAppService _appService = appService;

    public override async Task<TenantResponse> GetById(TenantRequestById request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out var tenantId)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid tenant id"));

        var tenant = await _appService.GetByIdAsync(tenantId) ?? throw new RpcException(new Status(StatusCode.NotFound, "Tenant not found"));

        return tenant;
    }

    public override async Task<TenantResponse> Create(TenantCreateRequest request, ServerCallContext context)
    {
        if (
            string.IsNullOrWhiteSpace(request.CompanyName)
            || string.IsNullOrWhiteSpace(request.TradingName)
            || string.IsNullOrWhiteSpace(request.TaxId)
            || string.IsNullOrWhiteSpace(request.Street)
            || string.IsNullOrWhiteSpace(request.Neighborhood)
            || string.IsNullOrWhiteSpace(request.City)
            || string.IsNullOrWhiteSpace(request.State)
            || string.IsNullOrWhiteSpace(request.PostalCode)
            ) throw new RpcException(new Status(StatusCode.InvalidArgument, "All arguments is requireds"));

        return await _appService.CreateAsync(request);
    }

    public override async Task<TenantResponse> Update(TenantUpdateRequest request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out _)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid TenantId"));

        if (
            string.IsNullOrWhiteSpace(request.CompanyName)
            || string.IsNullOrWhiteSpace(request.TradingName)
            || string.IsNullOrWhiteSpace(request.TaxId)
            || string.IsNullOrWhiteSpace(request.Street)
            || string.IsNullOrWhiteSpace(request.Neighborhood)
            || string.IsNullOrWhiteSpace(request.City)
            || string.IsNullOrWhiteSpace(request.State)
            || string.IsNullOrWhiteSpace(request.PostalCode)
            ) throw new RpcException(new Status(StatusCode.InvalidArgument, "All arguments is requireds"));

        var result = await _appService.UpdateAsync(request) ?? throw new RpcException(new Status(StatusCode.NotFound, "Tenant not found"));

        return result;
    }

    public override async Task<TenantDeleteResponse> Delete(TenantDeleteRequest request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out Guid id)) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid TenantId"));

        await _appService.DeleteAsync(id);

        return new TenantDeleteResponse();
    }
}
