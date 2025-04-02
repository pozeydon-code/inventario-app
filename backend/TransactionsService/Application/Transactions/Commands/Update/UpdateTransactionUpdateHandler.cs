
using Application.Transactions.Interfaces;
using Domain.DomainErrors;
using Domain.Models;
using Domain.Primitives;
using Domain.ValueObjects;
using TransactionsService.Application.ProductsGateway;

namespace Application.Transactions.Commands.Update;

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, ErrorOr<Unit>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IProductsApiClient _productsApiClient;
    private readonly IUnitOfWork _unitOfWork;


    public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IProductsApiClient productsApiClient)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _productsApiClient = productsApiClient ?? throw new ArgumentNullException(nameof(productsApiClient));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        if (!await _transactionRepository.ExistAsync(new TransactionId(request.Id)))
            return Errors.Transactions.NotFound;

        var currentStock = await _productsApiClient.GetStockAsync(request.Data.ProductId);
        if (currentStock == null)
            return Errors.Transactions.ProductNotFound;

        if (Detail.Create(request.Data.Detail) is not Detail detail)
            return Errors.Transactions.DetailWithBadFormat;

        if (Quantity.Create(request.Data.Quantity) is not Quantity quantity)
            return Errors.Transactions.QuantityWithBadFormat;

        if (UnitPrice.Create(request.Data.UnitPrice) is not UnitPrice unitPrice)
            return Errors.Transactions.PriceWithBadFormat;

        if (request.Data.Type == TransactionType.Sell)
        {


            int updatedStock = request.Data.Type switch
            {
                TransactionType.Buy => currentStock.Value + request.Data.Quantity,
                TransactionType.Sell => currentStock.Value - request.Data.Quantity,
                _ => currentStock.Value
            };

            if (updatedStock < 0)
                return Errors.Transactions.OutStock;

            var success = await _productsApiClient.UpdateStockAsync(request.Data.ProductId, updatedStock);
            if (!success)
                return Errors.Transactions.CantUpdate;
        }

        Transaction transaction = Transaction.UpdateTransaction(request.Id, request.Data.Date, request.Data.Type, request.Data.ProductId, quantity, unitPrice, detail);

        _transactionRepository.Update(transaction);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;


    }
}
