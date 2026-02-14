using System;
using System.Reflection;

namespace MonolithTemplate.Identity.Infrastructure;

public static class IdentityAssembly
{
    public static readonly Assembly GetAssembly = typeof(IdentityAssembly).Assembly;
}
