using MailKit.Security;
using MonolithTemplate.Notifications.Infrastructure.Services;
using Xunit;

namespace MonolithTemplate.Notifications.Tests.Infrastructure;

public class MailerSecurityTests
{
    [Theory]
    [InlineData("STARTTLS", SecureSocketOptions.StartTls)]
    [InlineData("StartTls", SecureSocketOptions.StartTls)]
    [InlineData("ssl", SecureSocketOptions.SslOnConnect)]
    [InlineData("StartTlsWhenAvailable", SecureSocketOptions.StartTlsWhenAvailable)]
    [InlineData("none", SecureSocketOptions.None)]
    [InlineData("", SecureSocketOptions.None)]
    public void ParseSecurity_ShouldMapSupportedValues(string input, SecureSocketOptions expected)
    {
        var actual = Mailer.ParseSecurity(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseSecurity_ShouldThrowForUnsupportedValue()
    {
        Assert.Throws<InvalidOperationException>(() => Mailer.ParseSecurity("weird-mode"));
    }
}
