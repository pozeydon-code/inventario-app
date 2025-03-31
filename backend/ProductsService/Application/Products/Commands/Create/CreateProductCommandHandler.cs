using Domain.Models;
using Domain.DomainErrors;
using Domain.Primitives;
using Application.Products.Interfaces;
using Domain.ValueObjects;

namespace Application.Products.Commands.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Guid>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (Name.Create(request.Name) is not Name name)
            return Errors.Products.NameWithBadFormat;

        if (Description.Create(request.Description) is not Description description)
            return Errors.Products.DescriptionWithBadFormat;

        if (CategoryName.Create(request.Category) is not CategoryName category)
            return Errors.Products.CategoryWithBadFormat;

        if (ProductPrice.Create(request.Price) is not ProductPrice price)
            return Errors.Products.PriceWithBadFormat;

        if (StockQuantity.Create(request.Stock) is not StockQuantity stock)
            return Errors.Products.StockWithBadFormat;

        var extension = Path.GetExtension(request.Image.FileName);
        var uniqueFileName = $"{Guid.NewGuid()}{extension}";

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await request.Image.CopyToAsync(fileStream);
        }

        var relativePath = $"/images/{uniqueFileName}";
        if (ImageUrl.Create(relativePath) is not ImageUrl image)
            return Errors.Products.ImageWithBadFormat;

        Product product = new(
            new ProductId(Guid.NewGuid()),
            name,
            description,
            category,
            image,
            price,
            stock
        );

        await _productRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id.Value;
    }
}
