using Microsoft.Extensions.Options;
using MonolithTemplate.Notifications.Infrastructure.Frontend;
using Xunit;

namespace MonolithTemplate.Notifications.Tests.Infrastructure;

public class FrontendUrlProviderTests
{
    [Fact]
    public void BuildConfirmationUrl_ShouldUseConfiguredBaseUrl()
    {
        var provider = CreateProvider("https://app.example.com/");
        var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var url = provider.BuildConfirmationUrl(userId, "abc+/=?");

        Assert.Equal("https://app.example.com/confirm?userId=11111111-1111-1111-1111-111111111111&token=abc%2B%2F%3D%3F", url);
    }

    [Fact]
    public void BuildResetPasswordUrl_ShouldFallBackToLocalhostWhenBaseUrlMissing()
    {
        var provider = CreateProvider(string.Empty);
        var userId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        var url = provider.BuildResetPasswordUrl(userId, "reset-token");

        Assert.Equal("https://localhost:3000/reset-password?userId=22222222-2222-2222-2222-222222222222&token=reset-token", url);
    }

    private static FrontendUrlProvider CreateProvider(string baseUrl)
    {
        var options = Options.Create(new FrontendOptions { BaseUrl = baseUrl });
        return new FrontendUrlProvider(options);
    }
}
