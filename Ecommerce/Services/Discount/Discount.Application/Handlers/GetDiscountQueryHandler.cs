using Discount.Application.DTOs;
using Discount.Application.Extensions;
using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers;

public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponDto>
{
    private readonly IDiscountRepository _repository;

    public GetDiscountQueryHandler(IDiscountRepository repository)
    {
        _repository = repository;
    }

    public async Task<CouponDto> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.ProductName))
        {
            var validationErrors = new Dictionary<string, string>()
            {
                { "ProductName", "Product name must not be empty." }
            };
            throw GrpcErrorHelper.CreateValidationException(validationErrors);
        }

        var coupon = await _repository.GetDiscount(request.ProductName)
            ?? throw new RpcException(
                new Status(StatusCode.Internal, $"Could not create discount for product: {request.ProductName}"));

        return coupon.ToDto();
    }
}
