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
        result.Should().Succeed().Which.Should().Be(value);
    }

    [Fact]
    public void WhenResultIsExpectedToBeSuccessWithValueItShouldBeSuccessWithValue()
    {
        string value = "value";
        var result = Result.Success<string, Exception>(value);

        result.Should().SucceedWith(value);
        result.Should().SucceedWith(value).Which.Should().Be(value);
    }

    [Fact]
    public void WhenResultIsExpectedToBeSuccessItShouldThrowWhenFailure()
    {
        var error = new Exception("error");
        var result = Result.Failure<string, Exception>(error);

        var action = () => result.Should().Succeed();

        action.Should().Throw<XunitException>().WithMessage(@$"Expected {nameof(result)} to succeed, but it failed with error ""System.Exception: error""");
    }

    [Fact]
    public void WhenResultIsExpectedToBeSuccessWithValueItShouldThrowWhenSuccessWithDifferentValue()
    {
        string value = "value";
        var result = Result.Success<string, Exception>(value);

        var action = () => result.Should().SucceedWith("some other value");

        action.Should().Throw<XunitException>().WithMessage(@$"Expected {nameof(result)} value to be ""some other value"", but found ""value""");
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
        result.Should().Fail().Which.Should().Be(error);
        result.Should().FailWith(error).Which.Should().Be(error);
    }

    [Fact]
    public void WhenResultIsExpectedToBeFailureWithValueItShouldBeFailureWithValue()
    {
        var error = new Exception("error");
        var result = Result.Failure<string, Exception>(error);

        result.Should().FailWith(error);
    }

    [Fact]
    public void WhenResultIsExpectedToBeFailureWithValueItShouldThrowWhenFailureWithDifferenceValue()
    {
        var error = new Exception("error");
        var result = Result.Failure<string, Exception>(error);

        var action = () => result.Should().FailWith(new Exception("Some other error"));

        action.Should().Throw<XunitException>().WithMessage(@$"Expected {nameof(result)} error to be System.Exception: Some other error, but found System.Exception: error");
    }

    [Fact]
    public void WhenResultIsExpectedToBeFailureItShouldThrowWhenSuccess()
    {
        string value = "value";
        var someError = new Exception("error");
        var result = Result.Success<string, Exception>(value);

        var action = () => result.Should().Fail();
        var actionWith = () => result.Should().FailWith(someError);

        action.Should().Throw<XunitException>().WithMessage(@$"Expected {nameof(result)} to fail, but it succeeded with value ""{value}""");
        actionWith.Should().Throw<XunitException>().WithMessage(@$"Expected {nameof(result)} to fail, but it succeeded");
    }
}
