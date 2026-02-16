using MonolithTemplate.Shared.ResultPattern;

public sealed record Email
{
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<Email>.Failure(Error.Validation("Email.EmptyException", "Email nie może być pusty!"));

        if (!value.Contains("@"))
            return Result<Email>.Failure(Error.Validation("Email.InvalidFormat", "Email musi zawierać @!"));

        return Result<Email>.Success(new(value));
    }

    public override string ToString() => Value;
}
