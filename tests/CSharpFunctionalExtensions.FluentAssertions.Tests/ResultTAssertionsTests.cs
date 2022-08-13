using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace CSharpFunctionalExtensions.FluentAssertions.Tests;

public class ResultAssertionsTests
{
    [Fact]
    public void WhenResultIsExpectedToHaveValueItShouldBeSuccessful()
    {
        var x = Result.Success("test");

        x.Should().Succeed();
    }

    [Fact]
    public void WhenResultIsExpectedToHaveValueItShouldNotBeFailure()
    {
        var x = Result.Success("test");

        var action = () => x.Should().Fail();

        action.Should().Throw<XunitException>().WithMessage("Expected Result to be failure but it succeeded");
    }

    [Fact]
    public void WhenResultIsExpectedToHaveValueItShouldBeSuccessfulWithValue()
    {
        string expected = "test";
        var x = Result.Success(expected);

        x.Should().SucceedWith(expected);
    }

    [Fact]
    public void WhenResultIsExpectedToHaveValueItShouldNotBeSuccessfulWithDifferentValue()
    {
        var x = Result.Success("foo");

        var action = () => x.Should().SucceedWith("bar");

        action.Should().Throw<XunitException>().WithMessage(@"Excepted Result value to be ""bar"" but found ""foo""");
    }

    [Fact]
    public void WhenResultIsExpectedToHaveErrorItShouldNotBeFailure()
    {
        var x = Result.Failure<string>("error");

        x.Should().Fail();
    }

    [Fact]
    public void WhenResultIsExpectedToHaveErrorItShouldBeFailure()
    {
        var x = Result.Failure<string>("error");

        var action = () => x.Should().Succeed();

        action.Should().Throw<XunitException>().WithMessage("Expected Result to be successful but it failed");
    }
}
