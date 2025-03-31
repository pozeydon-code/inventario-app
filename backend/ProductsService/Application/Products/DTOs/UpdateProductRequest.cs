
using Microsoft.AspNetCore.Http;

namespace Application.Products.DTOs;

public record UpdateProductRequest(
    string Name,
    string Description,
    string Category,
    IFormFile Image,
    decimal Price,
    int Stock) : IRequest<ErrorOr<Unit>>;
