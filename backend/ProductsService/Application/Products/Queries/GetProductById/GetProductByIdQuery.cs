using Application.Products.DTOs;

namespace Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ErrorOr<ProductResponse>>;
