namespace Domain.ValueObjects;

public partial record Name
{
    public string Value { get; init; }
    private Name(string value) => Value = value;

    public static Name? Create(string value)
    {
        if (string.IsNullOrEmpty(value) && value.Length > 50)
            return null;

        return new Name(value);
    }

    public static implicit operator string(Name name) => name.Value;
    public override string ToString() => Value;
}
