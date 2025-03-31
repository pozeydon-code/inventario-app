namespace Application.Products.DTOs;
public record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    string Category,
    string Image,
    decimal Price,
    int Stock);
