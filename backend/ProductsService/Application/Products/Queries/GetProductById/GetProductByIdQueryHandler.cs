

using Application.Products.DTOs;
using Application.Products.Interfaces;
using Application.Products.Queries.GetProductById;
using Domain.DomainErrors;
using Domain.Models;

namespace Application.Customers.GetById;

internal sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ErrorOr<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetCustomerByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ErrorOr<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        if (await _productRepository.GetByIdAsync(new ProductId(request.Id)) is not Product product)
            return Errors.Products.NotFound;

        return new ProductResponse(
            product.Id.Value,
            product.Name,
            product.Description,
            product.Category,
            product.Image,
            product.Price,
            product.Stock
            );
    }
}
