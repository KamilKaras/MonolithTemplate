namespace MonolithTemplate.Identity.Api.Requests;

public sealed record ResetPasswordRequest(string Password, string ConfirmPassword);
