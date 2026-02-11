
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace MonolithTemplate.Shared.ResultPattern;

public static class ResultExtensions
{
    public static IResult Match(
        this Result result,
        HttpContext httpContext,
        Func<IResult> onSuccess
    )
    {
        if (result.IsSuccess)
            return onSuccess();

        return Results.Problem(FromError(result.Error!, httpContext));
    }

    public static IResult Match<TValue>(
      this Result<TValue> result,
      HttpContext httpContext,
      Func<TValue, IResult> onSuccess
    )
    {
        if (result.IsSuccess)
            return onSuccess(result.Value);

        return Results.Problem(FromError(result.Error!, httpContext));
    }

    public static ProblemDetails FromError(this Error error, HttpContext httpContext)
    {
        var problemDetails = new ProblemDetails
        {
            Status = MapStatusFromType(error.Type),
            Title = error.Code,
            Detail = error.Description,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

        return problemDetails;
    }

    public static int MapStatusFromType(ErrorType type)
    {
        return type switch
        {
            ErrorType.BadRequest => StatusCodes.Status400BadRequest,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
    }

}

