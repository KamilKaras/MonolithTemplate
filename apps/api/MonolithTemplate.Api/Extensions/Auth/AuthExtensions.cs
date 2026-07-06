using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MonolithTemplate.Api.Extensions.Auth;

public static class AuthExtensions
{
    public static IServiceCollection AddAppAuth(this IServiceCollection services,
        IConfiguration configuration)
    {
        var issuer = GetRequiredSetting(configuration, "Jwt:Issuer");
        var audience = GetRequiredSetting(configuration, "Jwt:Audience");
        var key = GetRequiredSetting(configuration, "Jwt:Key");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key))
            };
            options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.TryGetValue("access_token", out var token))
                {
                    context.Token = token;
                }

                return Task.CompletedTask;
            }
        };
        });

        services.AddAuthorization();
        return services;
    }

    private static string GetRequiredSetting(IConfiguration configuration, string key)
    {
        var value = configuration[key];

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"Missing required configuration value: {key}", nameof(configuration));
        }

        return value;
    }
}
