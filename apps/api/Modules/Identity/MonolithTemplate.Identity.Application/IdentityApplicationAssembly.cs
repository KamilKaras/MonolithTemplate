using System;
using System.Reflection;

namespace MonolithTemplate.Identity.Application;

public static class IdentityApplicationAssembly
{
    public static Assembly GetAssembly() => typeof(IdentityApplicationAssembly).Assembly;
}
