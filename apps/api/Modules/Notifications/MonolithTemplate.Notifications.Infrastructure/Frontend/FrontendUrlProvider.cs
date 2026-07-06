using Microsoft.Extensions.Options;
using MonolithTemplate.Notifications.Application.Services;

namespace MonolithTemplate.Notifications.Infrastructure.Frontend;

public sealed class FrontendUrlProvider : IFrontendUrlProvider
{
    private readonly string _baseUrl;

    public FrontendUrlProvider(IOptions<FrontendOptions> options)
    {
        _baseUrl = NormalizeBaseUrl(options.Value.BaseUrl);
    }

    public string BuildConfirmationUrl(Guid userId, string confirmationToken)
    {
        return BuildUrl(
            "confirm",
            new Dictionary<string, string>
            {
                ["userId"] = userId.ToString(),
                ["token"] = confirmationToken
            });
    }

    public string BuildResetPasswordUrl(Guid userId, string resetToken)
    {
        return BuildUrl(
            "reset-password",
            new Dictionary<string, string>
            {
                ["userId"] = userId.ToString(),
                ["token"] = resetToken
            });
    }

    private string BuildUrl(string path, Dictionary<string, string> query)
    {
        var queryString = string.Join("&", query.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
        return $"{_baseUrl}/{path}?{queryString}";
    }

    private static string NormalizeBaseUrl(string? baseUrl)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            return "https://localhost:3000";
        }

        return baseUrl.TrimEnd('/');
    }
}
