using Microsoft.AspNetCore.Http;

namespace Application.Products.Commands.Update;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    string Category,
    IFormFile Image,
    decimal Price,
    int Stock) : IRequest<ErrorOr<Unit>>;
