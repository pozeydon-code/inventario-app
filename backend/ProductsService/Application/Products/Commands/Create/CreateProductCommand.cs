using Microsoft.AspNetCore.Http;

namespace Application.Products.Commands.Create;

public record CreateProductCommand(
    string Name,
    string Description,
    string Category,
    IFormFile Image,
    decimal Price,
    int Stock) : IRequest<ErrorOr<Guid>>;
