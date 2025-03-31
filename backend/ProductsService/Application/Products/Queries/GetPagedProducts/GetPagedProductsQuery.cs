using Application.Products.DTOs;

namespace Application.Products.Commands.GetPagedProducts;
public record GetPagedProductsQuery(
    int Page,
    int PageSize,
    string? Search
    ) : IRequest<PageResponse<ProductResponse>>;
