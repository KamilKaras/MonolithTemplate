using System;

namespace MonolithTemplate.Api.Extensions.CORS;

public static class CorsExtensions
{
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("Frontend", policy =>
            {
                policy
                    .WithOrigins("http://localhost:3000", "https://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        return services;
    }
}
