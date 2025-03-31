
using Application.Products.Commands.Update.UpdateStock;
using Application.Products.Interfaces;
using Domain.DomainErrors;
using Domain.Models;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Application.Products.Commands.Update;

public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStockCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    }

    public async Task<ErrorOr<Unit>> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
    {
        if( await _productRepository.GetByIdAsync(new ProductId(request.Id)) is not Product product)
            return Errors.Products.NotFound;
        
        product.UpdateStock(request.Stock);
        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
