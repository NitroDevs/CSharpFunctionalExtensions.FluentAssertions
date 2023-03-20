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

        action.Should().Throw<XunitException>().WithMessage("Expected result to be failure but it succeeded");
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

        action.Should().Throw<XunitException>().WithMessage(@"Expected Result value to be ""bar"" but found ""foo""");
    }

    [Fact]
    public void WhenResultIsExpectedToHaveErrorItShouldNotBeFailure()
    {
        var result = Result.Failure<string>("error");

        result.Should().Fail();
    }

    [Fact]
    public void WhenResultIsExpectedToHaveErrorItShouldBeFailure()
    {
        var result = Result.Failure<string>("error");

        var action = () => result.Should().Succeed();

        action.Should().Throw<XunitException>().WithMessage(@"Expected result to be successful but it failed with error ""error""");
    }
}
