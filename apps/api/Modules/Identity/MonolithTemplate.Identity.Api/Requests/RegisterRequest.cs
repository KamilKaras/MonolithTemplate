namespace MonolithTemplate.Identity.Api.Endpoints;

public sealed record RegisterRequest(string UserName, string Email, string Password, string ConfirmPassword);