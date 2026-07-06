using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonolithTemplate.Notifications.Application;
using MonolithTemplate.Notifications.Application.Services;
using MonolithTemplate.Notifications.Domain.Emails;
using MonolithTemplate.Notifications.Infrastructure.Frontend;
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

        services.AddMailer(configuration);
        return services;
    }


    private static IServiceCollection AddMailer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
        services.Configure<FrontendOptions>(configuration.GetSection("Frontend"));
        services.AddScoped<IMailer, Mailer>();
        services.AddScoped<IEmailTemplateRenderer, EmailTemplateRenderer>();
        services.AddScoped<IFrontendUrlProvider, FrontendUrlProvider>();
        return services;
    }
}
