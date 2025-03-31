namespace Domain.ValueObjects;

public partial record ImageUrl
{
    public string Value { get; init; }
    private ImageUrl(string value) => Value = value;

    public static ImageUrl? Create(string value)
    {
        if (string.IsNullOrEmpty(value) && value.Length > 255)
            return null;

        return new ImageUrl(value);
    }

    public static implicit operator string(ImageUrl imageUrl) => imageUrl.Value;
    public override string ToString() => Value;
}
