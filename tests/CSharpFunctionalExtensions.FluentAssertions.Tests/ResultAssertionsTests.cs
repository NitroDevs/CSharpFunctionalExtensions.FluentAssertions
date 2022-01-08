using System;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.FluentAssertions.Tests
{
    public class ResultAssertionsTests
    {
        [Fact]
        public void When_Result_Is_Expected_To_Have_Value_It_Should_be_Successful()
        {
            var x = Result.Success("test");

            x.Should().BeSuccessful();
        }

        [Fact]
        public void When_Result_Is_Expected_To_Have_Error_It_Should_Be_Failure()
        {
            var x = Result.Failure<string>("error");

            var action = () => x.Should().BeSuccessful();

            action.Should().Throw<ArgumentException>();
        }
    }
}
