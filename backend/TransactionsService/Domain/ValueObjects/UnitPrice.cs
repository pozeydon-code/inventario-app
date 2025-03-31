namespace Domain.ValueObjects;

public partial record UnitPrice
{
    public decimal Value { get; init; }
    private UnitPrice(decimal value) => Value = value;

    public static UnitPrice? Create(decimal value)
    {
        if (value < 0)
            return null;

        return new UnitPrice(value);
    }

    public static implicit operator decimal(UnitPrice unitPrice) => unitPrice.Value;
    public override string ToString() => Value.ToString();
}
