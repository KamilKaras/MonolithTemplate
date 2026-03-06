using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MonolithTemplate.Identity.Api.Requests;
using MonolithTemplate.Identity.Application.Features.UserForgetPassword;
using MonolithTemplate.Identity.Application.Features.UserLogin;
using MonolithTemplate.Identity.Application.Features.UserRegistration;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Api.Endpoints;

public static class IdentityEndpoints
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/identity").WithTags("Auth");

        group.MapPost("/register", async (
            [FromBody] RegisterRequest req,
            HttpContext ctx,
            IDispatcher dispatcher) =>
        {
            var result = await dispatcher.Send(
                new UserRegistrationCommand(req.UserName, req.Email, req.Password, req.ConfirmPassword)
                );

            return result.Match(
                httpContext: ctx,
                onSuccess: Results.Ok
            );

        });

        group.MapPost("/login", async (
                   [FromBody] LoginRequest req,
                   HttpContext ctx,
                   IDispatcher dispatcher) =>
        {
            var result = await dispatcher.Send(
                new UserLoginCommand(req.Email, req.Password)
            );

            return result.Match(
                httpContext: ctx,
                onSuccess: Results.Ok
            );
        });

        group.MapPost("/forget-password", async (
           [FromBody] ForgetPasswordRequest req,
           HttpContext ctx,
           IDispatcher dispatcher) =>
       {
           var result = await dispatcher.Send(
               new UserForgetPasswordCommand(req.Email)
               );

           return result.Match(
                httpContext: ctx,
                onSuccess: Results.Ok
            );
       });

        return app;
    }
}
