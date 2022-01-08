using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace CSharpFunctionalExtensions.FluentAssertions.Tests
{
    public class ResultAssertionsTests
    {
        [Fact]
        public void When_Result_Is_Expected_To_Have_Value_It_Should_Be_Successful()
        {
            var x = Result.Success("test");

            x.Should().BeSuccessful();
        }

        [Fact]
        public void When_Result_Is_Expected_To_Have_Value_It_Should_Not_Be_Failure()
        {
            var x = Result.Success("test");

            var action = () => x.Should().BeFailure();

            action.Should().Throw<XunitException>().WithMessage("Expected Result to be failure");
        }

        [Fact]
        public void When_Result_Is_Expected_To_Have_Value_It_Should_Be_Successful_With_Value()
        {
            string expected = "test";
            var x = Result.Success(expected);

            x.Should().BeSuccessfulWith(expected);
        }

        [Fact]
        public void When_Result_Is_Expected_To_Have_Value_It_Should_Not_Be_Successful_With_Different_Value()
        {
            var x = Result.Success("foo");

            var action = () => x.Should().BeSuccessfulWith("bar");

            action.Should().Throw<XunitException>().WithMessage(@"Excepted Result value to be ""bar"" but found ""foo""");
        }

        [Fact]
        public void When_Result_Is_Expected_To_Have_Error_It_Should_Not_Be_Failure()
        {
            var x = Result.Failure<string>("error");

            x.Should().BeFailure();
        }

        [Fact]
        public void When_Result_Is_Expected_To_Have_Error_It_Should_Be_Failure()
        {
            var x = Result.Failure<string>("error");

            var action = () => x.Should().BeSuccessful();

            action.Should().Throw<XunitException>().WithMessage("Expected Result to be successful but found error");
        }
    }
}
