using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace CSharpFunctionalExtensions.FluentAssertions.Tests;
public class UnitResultAssertionTests
{
    [Fact]
    public void WhenResultIsExpectedToBeSuccessItShouldBeSuccess()
    {
        var result = UnitResult.Success<string>();

        var action = () => result.Should().Succeed();

        action.Should().NotThrow();
    }

    [Fact]
    public void WhenResultIsExpectedToBeSuccessItShouldThrowWhenFailure()
    {
        string error = "error";
        var result = UnitResult.Failure(error);

        var action = () => result.Should().Succeed();

        action.Should().Throw<XunitException>().WithMessage(@$"Expected {nameof(result)} to succeed, but it failed with error ""{error}""");
    }

    [Fact]
    public void WhenResultIsExpectedToBeFailureItShouldBeFailure()
    {
        string error = "error";
        var result = UnitResult.Failure(error);

        var action = () => result.Should().Fail();
        var actionWithError = () => result.Should().FailWith(error);

        action.Should().NotThrow();
        actionWithError.Should().NotThrow();
    }

    [Fact]
    public void WhenResultIsExpectedToBeFailureWithValueItShouldBeFailureWithValue()
    {
        string error = "error";
        var result = UnitResult.Failure(error);

        result.Should().FailWith(error);
    }

    [Fact]
    public void WhenResultIsExpectedToBeFailureWithValueItShouldThrowWhenFailureWithDifferenceValue()
    {
        string error = "error";
        var result = UnitResult.Failure(error);

        var action = () => result.Should().FailWith("some other error");

        action.Should().Throw<XunitException>().WithMessage(@$"Expected {nameof(result)} error to be ""some other error"", but found ""{error}""");
    }

    [Fact]
    public void WhenResultIsExpectedToBeFailureItShouldThrowWhenSuccess()
    {
        var result = UnitResult.Success<string>();

        var action = () => result.Should().Fail();

        action.Should().Throw<XunitException>().WithMessage($"Expected {nameof(result)} to fail, but it succeeded");
    }
}
