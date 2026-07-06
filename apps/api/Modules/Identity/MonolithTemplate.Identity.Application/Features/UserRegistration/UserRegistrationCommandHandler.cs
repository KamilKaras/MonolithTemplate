using Microsoft.AspNetCore.Identity;
using MonolithTemplate.Identity.Contracts;
using MonolithTemplate.Identity.Domain.IdentityModels;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.Events;
using MonolithTemplate.Shared.OutboxPattern;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserRegistration;

public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, Result<Guid>>
{
    private readonly UserManager<User> _userManager;
    private readonly IOutbox _outbox;

    public UserRegistrationCommandHandler(
        UserManager<User> userManager,
        IOutbox outbox)
    {
        _userManager = userManager;
        _outbox = outbox;
    }
    public async Task<Result<Guid>> Handle(UserRegistrationCommand request, CancellationToken ct)
    {
        var validationResult = UserRegistrationCommandValidator.Validate(request);
        if (!validationResult.IsSuccess)
            return Result<Guid>.Failure(validationResult.Error!);

        if (request.Password != request.ConfirmPassword)
            return Error.BadRequest("Identity.PasswordNotMatch", "Podane hasła nie są takie same!");

        var user = new User
        {
            Email = request.Email,
            UserName = request.UserName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return Error.Failure("Identity.RegistrationFailed", "Wystąpił błąd podczas rejestracji");

        var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        _outbox.Enqueue(new UserRegisteredIntegrationEvent(user.Id, user.Email, confirmationToken));

        return user.Id;
    }
}
