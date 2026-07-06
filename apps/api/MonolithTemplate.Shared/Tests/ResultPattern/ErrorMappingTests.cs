using MonolithTemplate.Shared.ResultPattern;
using Xunit;

namespace MonolithTemplate.Shared.Tests.ResultPattern;

public sealed class ErrorMappingTests
{
    [Fact]
    public void Failure_ShouldMapToFailureType()
    {
        var error = Error.Failure("Test.Code", "Test error");

        Assert.Equal(ErrorType.Failure, error.Type);
    }
}
