using Application.Products.DTOs;

namespace Application.Products.Queries.GetAll;

public record GetAllProductsQuery() : IRequest<ErrorOr<IReadOnlyList<ProductResponse>>>;
