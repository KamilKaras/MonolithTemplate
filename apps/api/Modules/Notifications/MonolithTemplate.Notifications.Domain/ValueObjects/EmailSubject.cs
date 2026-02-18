using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Notifications.Domain.ValueObjects;

public sealed record EmailSubject
{
    private EmailSubject(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<EmailSubject> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<EmailSubject>.Failure(Error.BadRequest("EmailSubject.NullException", "Tutuł email nie może być pusty!"));

        return Result<EmailSubject>.Success(new EmailSubject(value));
    }

}
