using System.ComponentModel.DataAnnotations;

namespace MonolithTemplate.Identity.Application.Features.Validation;

internal static class IdentityValidationRules
{
    private static readonly EmailAddressAttribute EmailValidator = new();

    public static bool HasMinimumPasswordLength(string? password)
    {
        return !string.IsNullOrWhiteSpace(password) && password.Trim().Length >= 8;
    }

    public static bool IsValidEmail(string? email)
    {
        return !string.IsNullOrWhiteSpace(email) && EmailValidator.IsValid(email);
    }

    public static bool IsValidUserId(string? userId)
    {
        return !string.IsNullOrWhiteSpace(userId) && Guid.TryParse(userId, out _);
    }

    public static bool IsPresentToken(string? token)
    {
        return !string.IsNullOrWhiteSpace(token);
    }

    public static bool IsValidUserName(string? userName)
    {
        return !string.IsNullOrWhiteSpace(userName) && userName.Trim().Length >= 2;
    }
}
