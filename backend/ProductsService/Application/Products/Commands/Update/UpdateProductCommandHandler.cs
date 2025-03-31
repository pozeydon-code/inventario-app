
using Application.Products.Interfaces;
using Domain.DomainErrors;
using Domain.Models;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Application.Products.Commands.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    }


    public async Task<ErrorOr<Unit>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {

        if (!await _productRepository.ExistAsync(new ProductId(request.Id)))
            return Errors.Products.NotFound;

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

        if (await _productRepository.GetImageName(new ProductId(request.Id)) is not string imageName)
            return Errors.Products.ImageNotFound;

        var oldImagePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            imageName.TrimStart('/').Replace("/", "\\")
        );

        if (File.Exists(oldImagePath))
        {
            File.Delete(oldImagePath);
        }

        var extension = Path.GetExtension(request.Image.FileName);
        var uniqueFileName = $"{Guid.NewGuid()}{extension}";

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var newFilePath = Path.Combine(uploadsFolder, uniqueFileName);
        using (var fileStream = new FileStream(newFilePath, FileMode.Create))
        {
            await request.Image.CopyToAsync(fileStream);
        }

        var relativePath = $"/images/{uniqueFileName}";
        if (ImageUrl.Create(relativePath) is not ImageUrl image)
            return Errors.Products.ImageWithBadFormat;


        Product product = Product.UpdateProduct(request.Id, name, description, category, image, price, stock);

        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
