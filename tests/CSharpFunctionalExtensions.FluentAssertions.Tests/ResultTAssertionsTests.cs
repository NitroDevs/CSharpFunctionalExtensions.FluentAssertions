using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace CSharpFunctionalExtensions.FluentAssertions.Tests;

public class ResultAssertionsTests
{
    [Fact]
    public void WhenResultIsExpectedToHaveValueItShouldBeSuccessful()
    {
        var result = Result.Success("test");

        result.Should().Succeed();
    }

    [Fact]
    public void WhenResultIsExpectedToHaveValueItShouldNotBeFailure()
    {
        var result = Result.Success("test");

        var action = () => result.Should().Fail();

        action.Should().Throw<XunitException>().WithMessage(@$"Expected {nameof(result)} to fail, but it succeeded with value ""test""");
    }

    [Fact]
    public void WhenResultIsExpectedToHaveValueItShouldBeSuccessfulWithValue()
    {
        string expected = "test";
        var result = Result.Success(expected);

        result.Should().SucceedWith(expected);
    }

    [Fact]
    public void WhenResultIsExpectedToHaveValueItShouldNotBeSuccessfulWithDifferentValue()
    {
        var result = Result.Success("foo");

        var action = () => result.Should().SucceedWith("bar");

        action.Should().Throw<XunitException>().WithMessage(@$"Expected {nameof(result)} value to be ""bar"", but found ""foo""");
    }

    [Fact]
    public void WhenResultIsExpectedToHaveErrorFailShouldNotThrow()
    {
        string error = "error";
        var result = Result.Failure<int>(error);

        result.Should().Fail();
        result.Should().FailWith(error);
    }

    [Fact]
    public void WhenResultIsExpectedToHaveErrorSucceedShouldThrow()
    {
        var result = Result.Failure<int>("error");

        var action = () => result.Should().Succeed();

        action.Should().Throw<XunitException>().WithMessage(@$"Expected {nameof(result)} to succeed, but it failed with error ""error""");
    }
}
