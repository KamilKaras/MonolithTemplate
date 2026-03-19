using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonolithTemplate.Notifications.Application;
using MonolithTemplate.Notifications.Application.Services;
using MonolithTemplate.Notifications.Domain.Emails;
using MonolithTemplate.Notifications.Infrastructure.Services;
using MonolithTemplate.Shared;


namespace MonolithTemplate.Notifications.Infrastructure;

public static class Module
{
    public static IServiceCollection AddNotificationsModule(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = new[] { NotificationsInfrastructureAssembly.GetAssembly,
            NotificationsApplicationAssembly.GetAssembly
        };
        services.AddModuleShared(assemblies);

        var conn = configuration.GetConnectionString("default") ?? throw new ApplicationException("Connection string not found");

        services.AddMailer(configuration);
        return services;
    }


    private static IServiceCollection AddMailer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
        services.AddScoped<IMailer, Mailer>();
        services.AddScoped<IEmailTemplateRenderer, EmailTemplateRenderer>();
        return services;
    }
}
