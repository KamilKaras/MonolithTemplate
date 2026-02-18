using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Notifications.Domain.ValueObjects;

public sealed record EmailHtmlBody
{
    private EmailHtmlBody(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<EmailHtmlBody> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<EmailHtmlBody>.Failure(Error.BadRequest("EmailHtmlBody.NullException", "Email musi posiadać treść!"));

        return Result<EmailHtmlBody>.Success(new EmailHtmlBody(value));
    }

}
