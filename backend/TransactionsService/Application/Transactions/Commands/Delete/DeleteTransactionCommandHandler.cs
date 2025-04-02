
using Application.Transactions.Interfaces;
using Domain.DomainErrors;
using Domain.Models;
using Domain.Primitives;

namespace Application.Transactions.Commands.Delete;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, ErrorOr<Unit>>
{

    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        if (await _transactionRepository.GetByIdAsync(new TransactionId(request.Id)) is not Transaction transaction)
            return Errors.Transactions.NotFound;

        _transactionRepository.Delete(transaction);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;

    }
}
