using Application.Data;
using Application.Products.DTOs;
using Application.Products.Interfaces;

namespace Application.Products.Commands.GetPagedProducts;

public class GetPagedProductsQueryHandler : IRequestHandler<GetPagedProductsQuery, ErrorOr<PageResponse<ProductResponse>>>
{
    private readonly IProductRepository _productRepository;

    public GetPagedProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ErrorOr<PageResponse<ProductResponse>>> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetPagedAsync(request.Page, request.PageSize, request.Search);

        var total = await _productRepository.CountAsync(request.Search);

        return new PageResponse<ProductResponse>
        (
            products.Select(p => new ProductResponse(
                p.Id.Value,
                p.Name,
                p.Description,
                p.Category,
                p.Image,
                p.Price,
                p.Stock
            )).ToList(),
            total
        );
    }
}
