
using Application.Products.DTOs;
using Application.Products.Interfaces;
using Application.Products.Queries.GetAll;
using Domain.Models;

namespace Application.Products.Commands.GetAll;


internal sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ErrorOr<IReadOnlyList<ProductResponse>>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Product> products = await _productRepository.GetAllAsync();

        return products.Select(product => new ProductResponse(
                product.Id.Value,
                product.Name,
                product.Description,
                product.Category,
                product.Image,
                product.Price,
                product.Stock
            )).ToList();
    }
}
