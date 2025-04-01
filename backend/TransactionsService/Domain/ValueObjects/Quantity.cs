namespace Domain.ValueObjects;

public partial record Quantity
{
    public int Value { get; init; }

    private Quantity(int value) => Value = value;

    public static Quantity? Create(int value)
    {
        if (value < 0)
            return null;

        return new Quantity(value);
    }

    public static implicit operator int(Quantity quantity) => quantity.Value;
    public override string ToString() => Value.ToString("C");
}
