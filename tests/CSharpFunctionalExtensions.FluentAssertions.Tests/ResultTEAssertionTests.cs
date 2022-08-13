using System;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace CSharpFunctionalExtensions.FluentAssertions.Tests;
public class ResultTEAssertionTests
{
    [Fact]
    public void WhenResultIsExpectedToBeSuccessItShouldBeSuccess()
    {
        string value = "value";
        var result = Result.Success<string, Exception>(value);

        var action = () => result.Should().Succeed();

        action.Should().NotThrow();
    }

    [Fact]
    public void WhenResultIsExpectedToBeSuccessItShouldThrowWhenFailure()
    {
        var error = new Exception("error");
        var result = Result.Failure<string, Exception>(error);

        var action = () => result.Should().Succeed();

        action.Should().Throw<XunitException>().WithMessage("Expected Result to be successful but it failed");
    }

    [Fact]
    public void WhenResultIsExpectedToBeFailureItShouldBeFailure()
    {
        var error = new Exception("error");
        var result = Result.Failure<string, Exception>(error);

        var action = () => result.Should().Fail();
        var actionWith = () => result.Should().FailWith(error);

        action.Should().NotThrow();
        actionWith.Should().NotThrow();
    }

    [Fact]
    public void WhenResultIsExpectedToBeFailureItShouldThrowWhenSuccess()
    {
        string value = "value";
        var someError = new Exception("error");
        var result = Result.Success<string, Exception>(value);

        var action = () => result.Should().Fail();
        var actionWith = () => result.Should().FailWith(someError);

        action.Should().Throw<XunitException>().WithMessage("Expected Result to be failure but it succeeded");
        actionWith.Should().Throw<XunitException>().WithMessage("Expected Result to be failure but it succeeded");
    }
}
