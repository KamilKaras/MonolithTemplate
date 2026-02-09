namespace MonolithTemplate.Shared.CQRS.Abstractions;

public interface IDispatcher
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default);
}
