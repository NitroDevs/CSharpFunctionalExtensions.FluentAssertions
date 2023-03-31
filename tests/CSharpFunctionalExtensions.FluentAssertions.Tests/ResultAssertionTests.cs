using Xunit;
using Xunit.Sdk;
using FluentAssertions;

namespace CSharpFunctionalExtensions.FluentAssertions.Tests;

public class ResultAssertionTests
{
    [Fact]
    public void WhenResultIsExpectedToHaveValueItShouldBeSuccessful()
    {
        var result = Result.Success();

        result.Should().Succeed();
    }

    [Fact]
    public void WhenResultIsExpectedToHaveValueItShouldNotBeFailure()
    {
        var result = Result.Success();

        var action = () => result.Should().Fail();

        action.Should().Throw<XunitException>().WithMessage($"Expected {nameof(result)} to fail, but it succeeded");
    }

    [Fact]
    public void WhenResultIsExpectedToHaveErrorItShouldNotBeFailure()
    {
        var result = Result.Failure("error");

        result.Should().Fail();
    }

    [Fact]
    public void WhenResultIsExpectedToHaveErrorItShouldBeFailure()
    {
        var result = Result.Failure("error");

        var action = () => result.Should().Succeed();

        action.Should().Throw<XunitException>().WithMessage(@$"Expected {nameof(result)} to succeed, but it failed with error ""error""");
    }
}
