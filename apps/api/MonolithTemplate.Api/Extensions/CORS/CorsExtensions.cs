namespace MonolithTemplate.Api.Extensions.CORS;

public static class CorsExtensions
{
    public static IServiceCollection AddAppCors(this IServiceCollection services, IConfiguration configuration)
    {
        var frontendBaseUrl = configuration["Frontend:BaseUrl"];
        string[] origins = string.IsNullOrWhiteSpace(frontendBaseUrl)
            ? ["http://localhost:3000", "https://localhost:3000"]
            : [frontendBaseUrl];

        services.AddCors(options =>
        {
            options.AddPolicy("Frontend", policy =>
            {
                policy
                    .WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        return services;
    }
}
