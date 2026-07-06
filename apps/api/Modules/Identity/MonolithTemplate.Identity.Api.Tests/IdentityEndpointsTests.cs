using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MonolithTemplate.Identity.Api.Endpoints;
using MonolithTemplate.Identity.Api.Tests.Infrastructure;
using MonolithTemplate.Shared.Cqrs;
using Xunit;

namespace MonolithTemplate.Identity.Api.Tests;

public sealed class IdentityEndpointsTests
{
    private static async Task<HttpClient> CreateClientAsync()
    {
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();

        builder.Services
            .AddAuthentication(TestAuthHandler.SchemeName)
            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.SchemeName, _ => { });
        builder.Services.AddAuthorization();
        builder.Services.AddSingleton<IDispatcher, FakeDispatcher>();

        var app = builder.Build();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapIdentityEndpoints();

        await app.StartAsync();
        return app.GetTestClient();
    }

    [Fact]
    public async Task Register_HappyPath_ReturnsOk()
    {
        var client = await CreateClientAsync();

        var response = await client.PostAsJsonAsync("/identity/register", new
        {
            userName = "john",
            email = "john@example.com",
            password = "P@ssword123",
            confirmPassword = "P@ssword123"
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Register_MissingRequiredField_ReturnsBadRequest()
    {
        var client = await CreateClientAsync();

        var response = await client.PostAsJsonAsync("/identity/register", new
        {
            userName = "",
            email = "john@example.com",
            password = "P@ssword123",
            confirmPassword = "P@ssword123"
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_HappyPath_ReturnsOkAndSetsCookie()
    {
        var client = await CreateClientAsync();

        var response = await client.PostAsJsonAsync("/identity/login", new
        {
            email = "john@example.com",
            password = "P@ssword123"
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(response.Headers, h => h.Key == "Set-Cookie" && h.Value.Any(v => v.Contains("access_token=")));
    }

    [Fact]
    public async Task Login_MissingRequiredField_ReturnsBadRequest()
    {
        var client = await CreateClientAsync();

        var response = await client.PostAsJsonAsync("/identity/login", new
        {
            email = "",
            password = "P@ssword123"
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ForgotPassword_HappyPath_ReturnsOk()
    {
        var client = await CreateClientAsync();

        var response = await client.PostAsJsonAsync("/identity/forgot-password", new
        {
            email = "john@example.com"
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ForgotPassword_MissingRequiredField_ReturnsBadRequest()
    {
        var client = await CreateClientAsync();

        var response = await client.PostAsJsonAsync("/identity/forgot-password", new
        {
            email = ""
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ResetPassword_HappyPath_ReturnsOk()
    {
        var client = await CreateClientAsync();

        var response = await client.PostAsJsonAsync("/identity/reset-password", new
        {
            password = "P@ssword123",
            confirmPassword = "P@ssword123",
            userId = Guid.NewGuid().ToString(),
            token = "token"
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ResetPassword_MissingRequiredField_ReturnsBadRequest()
    {
        var client = await CreateClientAsync();

        var response = await client.PostAsJsonAsync("/identity/reset-password", new
        {
            password = "",
            confirmPassword = "P@ssword123",
            userId = Guid.NewGuid().ToString(),
            token = "token"
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ConfirmEmail_HappyPath_ReturnsOk()
    {
        var client = await CreateClientAsync();

        var response = await client.PostAsJsonAsync("/identity/confirm-email", new
        {
            userId = Guid.NewGuid().ToString(),
            token = "token"
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ConfirmEmail_MissingRequiredField_ReturnsBadRequest()
    {
        var client = await CreateClientAsync();

        var response = await client.PostAsJsonAsync("/identity/confirm-email", new
        {
            userId = "",
            token = "token"
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Me_Unauthenticated_ReturnsUnauthorized()
    {
        var client = await CreateClientAsync();

        var response = await client.GetAsync("/identity/me");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Me_Authenticated_ReturnsOk()
    {
        var client = await CreateClientAsync();
        client.DefaultRequestHeaders.Add("Authorization", TestAuthHandler.SchemeName);

        var response = await client.GetAsync("/identity/me");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
