
namespace MonolithTemplate.Notifications.Infrastructure.Services;

public sealed class FileEmailTemplateRenderer : IEmailTemplateRenderer
{
    private readonly string _templatesPath;

    public FileEmailTemplateRenderer()
    {
        _templatesPath = Path.Combine(AppContext.BaseDirectory, "Templates");
    }

    public async Task<string> RenderAsync(
        string templateName,
        Dictionary<string, string> variables,
        CancellationToken ct = default)
    {
        var filePath = Path.Combine(_templatesPath, $"{templateName}.html");

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Nie znaleziono template maila: {filePath}");

        var html = await File.ReadAllTextAsync(filePath, ct);

        foreach (var variable in variables)
        {
            html = html.Replace($"{{{{{variable.Key}}}}}", variable.Value);
        }

        return html;
    }
}