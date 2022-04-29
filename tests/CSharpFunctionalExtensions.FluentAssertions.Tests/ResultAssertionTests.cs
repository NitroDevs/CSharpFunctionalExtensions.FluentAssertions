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

        x.Should().Succeed();
    }

    [Fact]
    public void When_Result_Is_Expected_To_Have_Value_It_Should_Not_Be_Failure()
    {
        var x = Result.Success();

        var action = () => x.Should().Fail();

        action.Should().Throw<XunitException>().WithMessage("Expected Result to be failure but it succeeded");
    }

    [Fact]
    public void When_Result_Is_Expected_To_Have_Error_It_Should_Not_Be_Failure()
    {
        var x = Result.Failure("error");

        x.Should().Fail();
    }

    [Fact]
    public void When_Result_Is_Expected_To_Have_Error_It_Should_Be_Failure()
    {
        var x = Result.Failure("error");

        var action = () => x.Should().Succeed();

        action.Should().Throw<XunitException>().WithMessage("Expected Result to be successful but it failed");
    }
}
