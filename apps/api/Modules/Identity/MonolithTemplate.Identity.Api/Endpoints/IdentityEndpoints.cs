using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MonolithTemplate.Identity.Api.Requests;
using MonolithTemplate.Identity.Application.Features.UserRegistration;
using MonolithTemplate.Shared.Messaging;
using MonolithTemplate.Shared.Results;

namespace MonolithTemplate.Identity.Api.Endpoints;

public static class IdentityEndpoints
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth").WithTags("Auth");

        group.MapPost("/register", async (
            [FromBody] RegisterRequest req,
            IDispatcher dispatcher) =>
        {
            var result = await dispatcher.Send(
                new UserRegistrationCommand(req.UserName, req.Email, req.Password, req.ConfirmPassword)
                );

            Results.Ok(result);
        });

        return app;
    }
}
