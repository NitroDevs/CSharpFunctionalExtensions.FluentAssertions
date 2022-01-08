using System;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.FluentAssertions.Tests
{
    public class MaybeAssertionsTests
    {
        [Fact]
        public void When_Maybe_Is_Expected_To_Have_Value_And_It_Does_Should_Not_Throw()
        {
            var x = Maybe.From("test");

            x.Should().HaveValue("test");
        }

        [Fact]
        public void When_Maybe_Is_Expected_To_Have_Value_And_It_Has_Wrong_Value_Should_Throw()
        {
            var x = Maybe.From("oops");

            Action act = () => x.Should().HaveValue("test", "it is test");

            act.Should().Throw<Exception>().WithMessage($"*value \"test\" because it is test, but with value \"oops\" it*");
        }

        [Fact]
        public void When_Maybe_Is_Expected_To_Have_Value_And_It_Does_Not_Should_Throw()
        {
            Maybe<string> x = null;

            Action act = () => x.Should().HaveValue("test", "it is not None");

            act.Should().Throw<Exception>().WithMessage($"*value \"test\" because it is not None*");
        }

        [Fact]
        public void When_Maybe_Is_Expected_To_Have_No_Value_And_It_Has_None_Should_Not_Throw()
        {
            Maybe<string> x = null;

            x.Should().HaveNoValue();
        }

        [Fact]
        public void When_Maybe_Is_Expected_To_Have_No_Value_And_It_Has_One_Should_Throw()
        {
            var x = Maybe.From("test");

            Action act = () => x.Should().HaveNoValue("it is None");

            act.Should().Throw<Exception>().WithMessage($"*Maybe to have no value because it is None, but with value \"test\" it*");
        }
    }
}
