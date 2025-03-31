namespace Domain.ValueObjects;

public partial record Description
{
    private const int MaxLength = 500;
    public string Value { get; init; }
    private Description(string value) => Value = value;

    public static Description? Create(string value)
    {
        if (string.IsNullOrEmpty(value) && value.Length > MaxLength)
            return null;

        return new Description(value);
    }

    public static implicit operator string(Description description) => description.Value;
    public override string ToString() => Value;
}
