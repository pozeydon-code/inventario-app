namespace Domain.ValueObjects;

public partial record Quantity
{
    public decimal Value { get; init; }

    private Quantity(decimal value) => Value = value;

    public static Quantity? Create(decimal value)
    {
        if (value < 0)
            return null;

        return new Quantity(value);
    }

    public static implicit operator decimal(Quantity quantity) => quantity.Value;
    public override string ToString() => Value.ToString("C");
}
