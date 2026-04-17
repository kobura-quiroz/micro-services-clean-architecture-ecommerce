using Discount.Application.Commands;
using Discount.Application.Extensions;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers;

public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
{
    private readonly IDiscountRepository _repository;

    public DeleteDiscountCommandHandler(IDiscountRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.ProductName))
        {
            var validationErrors = new Dictionary<string, string>()
            {
                ["ProductName"] = "Product name must not be empty."
            };
            throw GrpcErrorHelper.CreateValidationException(validationErrors);
        }
        return await _repository.DeleteDiscount(request.ProductName);
    }
}
