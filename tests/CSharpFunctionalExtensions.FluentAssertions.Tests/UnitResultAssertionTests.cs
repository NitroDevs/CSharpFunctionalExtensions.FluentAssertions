using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace CSharpFunctionalExtensions.FluentAssertions.Tests;
public class UnitResultAssertionTests
{
    [Fact]
    public void WhenResultIsExpectedToBeSuccessItShouldBeSuccess()
    {
        var r = UnitResult.Success<string>();
        var action = () => r.Should().Succeed();

        action.Should().NotThrow();
    }

    [Fact]
    public void WhenResultIsExpectedToBeSuccessItShouldThrowWhenFailure()
    {
        string error = "error";
        var r = UnitResult.Failure(error);
        var action = () => r.Should().Succeed();

        action.Should().Throw<XunitException>().WithMessage("Expected UnitResult to be successful but it failed");
    }

    [Fact]
    public void WhenResultIsExpectedToBeFailureItShouldBeFailure()
    {
        string error = "error";
        var r = UnitResult.Failure(error);

        var action = () => r.Should().Fail();
        var actionWithError = () => r.Should().FailWith(error);

        action.Should().NotThrow();
        actionWithError.Should().NotThrow();
    }

    [Fact]
    public void WhenResultIsExpectedToBeFailureWithValueItShouldBeFailureWithValue()
    {
        string error = "error";
        var r = UnitResult.Failure(error);

        r.Should().FailWith(error);
    }

    [Fact]
    public void WhenResultIsExpectedToBeFailureItShouldThrowWhenSuccess()
    {
        var r = UnitResult.Success<string>();
        var action = () => r.Should().Fail();

        action.Should().Throw<XunitException>().WithMessage("Expected UnitResult to be failure but it succeeded");
    }
}
