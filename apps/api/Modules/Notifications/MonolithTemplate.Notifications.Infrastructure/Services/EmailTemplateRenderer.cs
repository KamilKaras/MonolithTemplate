using System.Reflection;
using System.Text;
using MonolithTemplate.Notifications.Application.Services;

namespace MonolithTemplate.Notifications.Infrastructure.Services;

public sealed class EmailTemplateRenderer : IEmailTemplateRenderer
{
    private readonly Assembly _assembly;

    public EmailTemplateRenderer()
    {
        _assembly = typeof(EmailTemplateRenderer).Assembly;
    }

    public async Task<string> RenderAsync(
        string templateName,
        Dictionary<string, string> variables,
        CancellationToken ct = default)
    {
        var expectedSuffix = $"EmailTemplates.{templateName}.html";

        var resourceName = _assembly
            .GetManifestResourceNames()
            .FirstOrDefault(x => x.EndsWith(expectedSuffix, StringComparison.OrdinalIgnoreCase));

        if (resourceName is null)
        {
            throw new FileNotFoundException(
                $"Nie znaleziono template maila: {expectedSuffix}. " +
                $"Dostępne zasoby: {string.Join(", ", _assembly.GetManifestResourceNames())}");
        }

        await using var stream = _assembly.GetManifestResourceStream(resourceName);

        if (stream is null)
            throw new InvalidOperationException($"Nie udało się otworzyć zasobu: {resourceName}");

        using var reader = new StreamReader(stream, Encoding.UTF8);
        var html = await reader.ReadToEndAsync(ct);

        foreach (var variable in variables)
        {
            html = html.Replace($"{{{{{variable.Key}}}}}", variable.Value);
        }

        return html;
    }
}