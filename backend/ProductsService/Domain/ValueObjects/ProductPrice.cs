namespace Domain.ValueObjects;

public partial record ProductPrice
{
    public decimal Value { get; init; }

    private ProductPrice(decimal value) => Value = value;

    public static ProductPrice? Create(decimal value)
    {
        if (value < 0)
            return null;

        return new ProductPrice(value);
    }

    public static implicit operator decimal(ProductPrice price) => price.Value;
    public override string ToString() => Value.ToString("C");
}
