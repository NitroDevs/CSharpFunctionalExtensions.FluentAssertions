using Xunit;
using Xunit.Sdk;
using FluentAssertions;

namespace CSharpFunctionalExtensions.FluentAssertions.Tests;

public class ResultAssertionTests
{
    [Fact]
    public void When_Result_Is_Expected_To_Have_Value_It_Should_Be_Successful()
    {
        var x = Result.Success();

        x.Should().BeSuccessful();
    }

    [Fact]
    public void When_Result_Is_Expected_To_Have_Value_It_Should_Not_Be_Failure()
    {
        var x = Result.Success();

        var action = () => x.Should().BeFailure();

        action.Should().Throw<XunitException>().WithMessage("Expected Result to be failure but found success");
    }

    [Fact]
    public void When_Result_Is_Expected_To_Have_Error_It_Should_Not_Be_Failure()
    {
        var x = Result.Failure("error");

        x.Should().BeFailure();
    }

    [Fact]
    public void When_Result_Is_Expected_To_Have_Error_It_Should_Be_Failure()
    {
        var x = Result.Failure("error");

        var action = () => x.Should().BeSuccessful();

        action.Should().Throw<XunitException>().WithMessage("Expected Result to be successful but found error");
    }
}
