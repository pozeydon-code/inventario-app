namespace Application.Products.DTOs;

public record PageResponse<T>(
    List<T> Items,
    int TotalCount
    );
