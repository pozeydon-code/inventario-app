using Application.Products.Interfaces;
using Domain.DomainErrors;
using Domain.Models;
using Domain.Primitives;

namespace Application.Products.Commands.Delete;

internal sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        if (await _productRepository.GetByIdAsync(new ProductId(request.Id)) is not Product product)
            return Errors.Products.NotFound;

        _productRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (await _productRepository.GetImageName(new ProductId(request.Id)) is not string imageName)
            return Unit.Value;

        var oldImagePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            imageName.TrimStart('/').Replace("/", "\\")
        );

        if (File.Exists(oldImagePath))
        {
            File.Delete(oldImagePath);
        }


        return Unit.Value;
    }
}
