namespace MonolithTemplate.Identity.Api.Requests;

public sealed record RegisterRequest(string UserName, string Email, string Password, string ConfirmPassword);