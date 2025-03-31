using Domain.Models;
using Domain.DomainErrors;
using Domain.Primitives;
using Application.Transactions.Interfaces;
using Domain.ValueObjects;
using TransactionsService.Application.ProductsGateway;

namespace Application.Transactions.Commands.Create;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, ErrorOr<Guid>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IProductsApiClient _productsApiClient;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTransactionCommandHandler(ITransactionRepository TransactionRepository, IUnitOfWork unitOfWork, IProductsApiClient productsApiClient)
    {
        _transactionRepository = TransactionRepository ?? throw new ArgumentNullException(nameof(TransactionRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _productsApiClient = productsApiClient ?? throw new ArgumentNullException(nameof(productsApiClient));

    }

    public async Task<ErrorOr<Guid>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        if (!await _transactionRepository.ExistByProductIdAsync(new Guid(request.ProductId)))
            return Errors.Transactions.ProductNotFound;

        if (Detail.Create(request.Detail) is not Detail detail)
            return Errors.Transactions.DetailWithBadFormat;

        if (Quantity.Create(request.Quantity) is not Quantity quantity)
            return Errors.Transactions.QuantityWithBadFormat;

        if (UnitPrice.Create(request.UnitPrice) is not UnitPrice unitPrice)
            return Errors.Transactions.PriceWithBadFormat;
        
        if (request.Type == TransactionType.Sell)
        {
            // TODO: Get the actual value in stock or 

            var currentStock = await _productsApiClient.GetStockAsync(new Guid(request.ProductId));
            if (currentStock == null)
                return Errors.Transactions.ProductNotFound;

            int updatedStock = request.Type switch
            {
                TransactionType.Buy => currentStock.Value + request.Quantity,
                TransactionType.Sell => currentStock.Value - request.Quantity,
                _ => currentStock.Value
            };

            if (updatedStock < 0)
                return Errors.Transactions.OutStock;
            
            var success = await _productsApiClient.UpdateStockAsync(new Guid(request.ProductId), updatedStock);
            if (!success)
                return Errors.Transactions.CantUpdate;
        }
        
       var transaction = new Transaction(
            new TransactionId(Guid.NewGuid()),
            request.Date,
            request.Type,
            new Guid(request.ProductId),
            quantity,
            unitPrice,
            detail
        );

        await _transactionRepository.AddAsync(transaction);

        // TODO: actualizar stock del producto según tipo (Compra suma, Venta resta)

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return transaction.Id.Value;
    }
}
