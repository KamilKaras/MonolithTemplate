using System;
using System.Reflection;

namespace MonolithTemplate.Identity.Infrastructure;

public static class IdentityApplicationAssembly
{
    public static readonly Assembly GetAssembly = typeof(IdentityApplicationAssembly).Assembly;
}
