using Microsoft.Extensions.DependencyInjection;

namespace MonolithTemplate.Shared.CQRS.Abstractions;

public sealed class Dispatcher(IServiceProvider sp) : IDispatcher
{
    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default)
    {
        var requestType = request.GetType();

        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        var handler = sp.GetRequiredService(handlerType);

        RequestHandlerDelegate<TResponse> handlerDelegate = () =>
        {
            var method = handlerType.GetMethod("Handle")!;
            return (Task<TResponse>)method.Invoke(handler, new object[] { request, ct })!;
        };

        var behaviorType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, typeof(TResponse));
        var behaviors = sp.GetServices(behaviorType).Reverse().ToArray();

        foreach (var behavior in behaviors)
        {
            var next = handlerDelegate;
            handlerDelegate = () =>
            {
                var method = behaviorType.GetMethod("Handle")!;
                return (Task<TResponse>)method.Invoke(behavior, new object[] { request, ct, next })!;
            };
        }

        return handlerDelegate();
    }
}