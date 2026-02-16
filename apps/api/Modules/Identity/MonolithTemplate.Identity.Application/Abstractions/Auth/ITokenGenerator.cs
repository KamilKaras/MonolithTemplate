using System;
using MonolithTemplate.Identity.Domain.IdentityModels;

namespace MonolithTemplate.Identity.Application.Abstractions.Auth;

public interface ITokenGenerator
{
    string Generate(User user);
}
