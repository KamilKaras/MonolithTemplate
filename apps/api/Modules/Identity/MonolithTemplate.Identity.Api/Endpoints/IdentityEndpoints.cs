using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using MonolithTemplate.Identity.Api.Requests;
using MonolithTemplate.Identity.Application.Features.GetUserCredentials;
using MonolithTemplate.Identity.Application.Features.ResetPassword;
using MonolithTemplate.Identity.Application.Features.UserConfirmEmail;
using MonolithTemplate.Identity.Application.Features.UserForgetPassword;
using MonolithTemplate.Identity.Application.Features.UserLogin;
using MonolithTemplate.Identity.Application.Features.UserRegistration;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;


namespace MonolithTemplate.Identity.Api.Endpoints {
    public static class IdentityEndpoints {
        public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app) {
            var group = app.MapGroup("/identity").WithTags("Auth");

            group.MapGet("/me", [Authorize] async (
                HttpContext ctx,
                IDispatcher dispatcher) => {
                    var result = await dispatcher.Send(
                        new GetUserCredentialsQuery()
                        );

                    return result.Match(
                         httpContext: ctx,
                         onSuccess: Results.Ok
                     );
                })
            .RequireAuthorization();

            group.MapPost("/register", async (
                [FromBody] RegisterRequest req,
                HttpContext ctx,
                IDispatcher dispatcher) => {
                    var result = await dispatcher.Send(
                        new UserRegistrationCommand(req.UserName, req.Email, req.Password, req.ConfirmPassword)
                        );

                    return result.Match(
                        httpContext: ctx,
                        onSuccess: Results.Ok
                    );

                });

            group.MapPost("/login", async (
                [FromBody] UserLoginCommand req,
                HttpContext ctx,
                HttpResponse response,
                IDispatcher dispatcher) => {
                    var result = await dispatcher.Send(req);

                    return result.Match(
                        httpContext: ctx,
                        onSuccess: () => {
                            response.Cookies.Append("access_token", result.Value.Token, new CookieOptions {
                                HttpOnly = true,
                                Secure = true,
                                SameSite = SameSiteMode.None,
                                Expires = DateTimeOffset.UtcNow.AddMinutes(30),
                                Path = "/"
                            });
                            return Results.Ok();
                        }
                    );
                });

            group.MapPost("/logout", (HttpResponse response) => {
                response.Cookies.Append("access_token", "", new CookieOptions {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(-1),
                    Path = "/"
                });
                return Results.Ok();
            });

            group.MapPost("/forgot-password", async (
               [FromBody] ForgotPasswordRequest req,
               HttpContext ctx,
               IDispatcher dispatcher) => {
                   var result = await dispatcher.Send(
                       new UserForgetPasswordCommand(req.Email)
                       );

                   return result.Match(
                        httpContext: ctx,
                        onSuccess: () => Results.Ok()
                    );
               });

            group.MapPost("/reset-password", async (
            [FromBody] ResetPasswordRequest req,
            HttpContext ctx,
            IDispatcher dispatcher) => {
                var result = await dispatcher.Send(
                    new ResetPasswordCommand(req.Password, req.ConfirmPassword, req.UserId, req.Token)
                    );

                return result.Match(
                     httpContext: ctx,
                     onSuccess: () => Results.Ok()
                 );
            });

            group.MapPost("/confirm-email", async (
                [FromBody] ConfirmEmailRequest req,
                HttpContext ctx,
                IDispatcher dispatcher) => {
                    var result = await dispatcher.Send(
                        new UserConfirmEmailCommand(req.UserId, req.Token)
                        );

                    return result.Match(
                         httpContext: ctx,
                         onSuccess: Results.Ok
                     );
                });

            return app;
        }
    }
}
