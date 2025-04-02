using Application.Transactions.DTOs;
using Application.Transactions.Interfaces;
using Domain.DomainErrors;
using Domain.Models;

namespace Application.Transactions.Queries.GetTransactionByIdQuery;

public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, ErrorOr<TransactionResponse>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionByIdQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
    }

    public async Task<ErrorOr<TransactionResponse>> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        if (await _transactionRepository.GetByIdAsync(new TransactionId(request.Id)) is not Transaction transaction)
            return Errors.Transactions.NotFound;

        return new TransactionResponse(
            transaction.Id.Value,
            transaction.Date,
            transaction.Type,
            transaction.ProductId,
            transaction.Quantity,
            transaction.UnitPrice,
            transaction.Detail
        );

    }
}
