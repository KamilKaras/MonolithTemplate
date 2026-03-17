namespace MonolithTemplate.Identity.Api.Requests;

public sealed record ConfirmEmailRequest(string UserId, string Token);