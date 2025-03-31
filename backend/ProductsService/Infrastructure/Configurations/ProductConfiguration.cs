using Domain.Models;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasConversion(
            productId => productId.Value,
            value => new ProductId(value));

        builder.Property(c => c.Name).HasConversion(
            name => name.Value,
            value => Name.Create(value)!).HasMaxLength(50);

        builder.Property(c => c.Description).HasConversion(
            description => description.Value,
            value => Description.Create(value)!).HasMaxLength(100);

        builder.Property(c => c.Image).HasConversion(
            image => image.Value,
            value => ImageUrl.Create(value)!
            );

        builder.Property(c => c.Category).HasConversion(
            category => category.Value,
            value => CategoryName.Create(value)!).HasMaxLength(100);

        builder.Property(c => c.Price).HasConversion(
            price => price.Value,
            value => ProductPrice.Create(value)!);

        builder.Property(c => c.Stock).HasConversion(
            stock => stock.Value,
            value => StockQuantity.Create(value)!);

    }
}
