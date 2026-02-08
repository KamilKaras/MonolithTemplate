using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MonolithTemplate.Identity.Infrastructure.IdentityModels;

namespace MonolithTemplate.Identity.Api.Endpoints;

public static class IdentityEndpoints
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth").WithTags("Auth");

        group.MapPost("/register", async (
            [FromBody] RegisterRequest req,
            UserManager<User> userManager) =>
        {
            var user = new User { Email = req.Email, UserName = req.UserName };
            var result = await userManager.CreateAsync(user, req.Password);
            return result.Succeeded
               ? Results.Ok()
               : Results.BadRequest(result.Errors);
        });

        return app;
    }
}
