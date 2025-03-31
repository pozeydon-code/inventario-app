using Domain.ValueObjects;

namespace Domain.Models;

public sealed class Product
{
    #region Propiedades
    public ProductId Id { get; private set; }
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public CategoryName Category { get; private set; }
    public ImageUrl Image { get; private set; }
    public ProductPrice Price { get; private set; }
    public StockQuantity Stock { get; private set; }
    #endregion

    #region Constructores
    private Product()
    {

    }

    public Product(ProductId id, Name name, Description description, CategoryName category, ImageUrl imageUrl, ProductPrice price, StockQuantity stock)
    {
        Id = id;
        Name = name;
        Description = description;
        Category = category;
        Image = imageUrl;
        Price = price;
        Stock = stock;
    }

    public static Product UpdateProduct(Guid id, Name name, Description description, CategoryName category, ImageUrl imageUrl, ProductPrice price, StockQuantity stock)
    {
        return new Product(new ProductId(id), name, description, category, imageUrl, price, stock);
    }

    #endregion

    #region Metodos

    public void ReduceStock(int cantidad) => Stock = Stock.Reduce(cantidad);
    public void IncreaseStock(int cantidad) => Stock = Stock.Increase(cantidad);
    public void UpdatePrice(ProductPrice newPrice) => Price = newPrice;
    public void UpdateStock(int newStock) => Stock = Stock.SetStock(newStock);

    #endregion

}
