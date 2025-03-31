namespace Domain.ValueObjects;

public partial record CategoryName
{
    public string Value { get; init; }
    private CategoryName(string value) => Value = value;

    public static CategoryName? Create(string value)
    {
        if (string.IsNullOrEmpty(value) && value.Length > 100)
            return null;

        return new CategoryName(value);
    }

    public static implicit operator string(CategoryName name) => name.Value;
    public override string ToString() => Value;
}
