namespace Domain.ValueObjects;

public partial record Detail
{
    private const int MaxLength = 255;
    public string Value { get; init; }
    private Detail(string value) => Value = value;

    public static Detail? Create(string value)
    {
        if (string.IsNullOrEmpty(value) && value.Length > MaxLength)
            return null;

        return new Detail(value);
    }

    public static implicit operator string(Detail detail) => detail.Value;
    public override string ToString() => Value;
}
