using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MonolithTemplate.Shared.OutboxPattern;

public sealed class OutboxProcessor : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<OutboxProcessor> _logger;

    public OutboxProcessor(
        IServiceScopeFactory scopeFactory,
        ILogger<OutboxProcessor> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var modules = scope.ServiceProvider.GetServices<IOutboxModule>().ToArray();

                var processed = 0;

                foreach (var module in modules)
                    processed += await module.ProcessAsync(stoppingToken);

                await Task.Delay(processed > 0 ? 50 : 500, stoppingToken);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Global outbox processor failed");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
