namespace MonolithTemplate.Shared.Results;

public class Error
{
    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }

    public static Error Failure(string code, string description) =>
        new(code, description, ErrorType.Failure);
    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);
    public static Error Validation(string code, string description) =>
        new(code, description, ErrorType.Validation);
    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);
    public static Error AccessUnAuthorized(string code, string description) =>
        new(code, description, ErrorType.AccessUnAuthorized);
    public static Error AccessForbidden(string code, string description) =>
        new(code, description, ErrorType.AccessForbidden);

}
