namespace MonolithTemplate.Notifications.Application.Services;

public interface IEmailTemplateRenderer
{
    Task<string> RenderAsync(string templateName, Dictionary<string, string> variables, CancellationToken ct = default);
}
