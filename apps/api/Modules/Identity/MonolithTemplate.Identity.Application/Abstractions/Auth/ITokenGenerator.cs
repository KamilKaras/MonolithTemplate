using System;
using MonolithTemplate.Identity.Domain.IdentityModels;

namespace MonolithTemplate.Identity.Application.Abstractions.AccessToken;

public interface ITokenGenerator
{
    string Generate(User user);
}
