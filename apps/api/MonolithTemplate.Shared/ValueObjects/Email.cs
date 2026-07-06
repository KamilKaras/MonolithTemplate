using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Shared.ValueObjects;

public sealed record Email
{
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email> Create(string value)
    {
        var trimmed = value.Trim();
        if (string.IsNullOrWhiteSpace(trimmed))
            return Error.Validation("Email.EmptyException", "Email nie może być pusty!");

        if (!trimmed.Contains("@"))
            return Error.Validation("Email.InvalidFormat", "Email musi zawierać @!");

        return new Email(trimmed);
    }

    public override string ToString() => Value;
}
