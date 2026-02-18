using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Notifications.Domain.ValueObjects;

public sealed record EmailTextBody
{
    private EmailTextBody(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<EmailTextBody> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<EmailTextBody>.Failure(Error.BadRequest("EmailTextBody.NullException", "Email musi posiadać treść!"));

        return Result<EmailTextBody>.Success(new EmailTextBody(value));
    }

}
