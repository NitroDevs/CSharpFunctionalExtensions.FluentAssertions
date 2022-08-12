using Xunit;
using Xunit.Sdk;
using FluentAssertions;

namespace CSharpFunctionalExtensions.FluentAssertions.Tests;

public class ResultAssertionTests
{
    [Fact]
    public void WhenResultIsExpectedToHaveValueItShouldBeSuccessful()
    {
        var x = Result.Success();

        x.Should().Succeed();
    }

    [Fact]
    public void WhenResultIsExpectedToHaveValueItShouldNotBeFailure()
    {
        var x = Result.Success();

        var action = () => x.Should().Fail();

        action.Should().Throw<XunitException>().WithMessage("Expected Result to be failure but it succeeded");
    }

    [Fact]
    public void WhenResultIsExpectedToHaveErrorItShouldNotBeFailure()
    {
        var x = Result.Failure("error");

        x.Should().Fail();
    }

    [Fact]
    public void WhenResultIsExpectedToHaveErrorItShouldBeFailure()
    {
        var x = Result.Failure("error");

        var action = () => x.Should().Succeed();

        action.Should().Throw<XunitException>().WithMessage("Expected Result to be successful but it failed");
    }
}
