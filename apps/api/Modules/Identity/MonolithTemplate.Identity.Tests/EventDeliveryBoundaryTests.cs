using MonolithTemplate.Identity.Application.Features.UserForgetPassword;
using MonolithTemplate.Identity.Application.Features.UserRegistration;
using MonolithTemplate.Shared.Events;
using MonolithTemplate.Shared.OutboxPattern;
using Xunit;

namespace MonolithTemplate.Identity.Tests;

public sealed class EventDeliveryBoundaryTests
{
    [Fact]
    public void ForgotPasswordHandler_UsesOutboxBoundary()
    {
        var parameterTypes = typeof(UserForgetPasswordCommandHandler)
            .GetConstructors()
            .Single()
            .GetParameters()
            .Select(parameter => parameter.ParameterType)
            .ToArray();

        Assert.Contains(typeof(IOutbox), parameterTypes);
        Assert.DoesNotContain(typeof(IEventBus), parameterTypes);
    }

    [Fact]
    public void RegistrationHandler_UsesOutboxBoundary()
    {
        var parameterTypes = typeof(UserRegistrationCommandHandler)
            .GetConstructors()
            .Single()
            .GetParameters()
            .Select(parameter => parameter.ParameterType)
            .ToArray();

        Assert.Contains(typeof(IOutbox), parameterTypes);
        Assert.DoesNotContain(typeof(IEventBus), parameterTypes);
    }
}
