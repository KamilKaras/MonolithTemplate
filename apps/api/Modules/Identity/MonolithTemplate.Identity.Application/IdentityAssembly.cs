using System.Reflection;

namespace MonolithTemplate.Identity.Application;

public static class IdentityAssembly
{
    public static readonly Assembly GetAssembly = typeof(IdentityAssembly).Assembly;
}
