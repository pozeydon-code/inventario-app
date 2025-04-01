namespace Application.Transactions.DTOs;

public record PagedResponse<T>(
    List<T> Items,
    int TotalCount
);
