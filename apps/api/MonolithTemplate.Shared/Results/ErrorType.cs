namespace MonolithTemplate.Shared.Results;

public enum ErrorType
{
    Failure = 0,
    NotFound,
    Validation,
    Conflict,
    AccessUnAuthorized,
    AccessForbidden
}
