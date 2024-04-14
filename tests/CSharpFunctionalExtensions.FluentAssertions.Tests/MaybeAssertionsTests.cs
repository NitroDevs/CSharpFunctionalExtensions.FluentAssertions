using System;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.FluentAssertions.Tests;

public class MaybeAssertionsTests
{
    [Fact]
    public void WhenMaybeIsExpectedToHaveSomeValueAndItDoesShouldNotThrow()
    {
        var maybe = Maybe.From("test");

        maybe.Should().HaveSomeValue();
        maybe.Should().HaveSomeValue().Which.Should().Be("test");
    }

    [Fact]
    public void WhenMaybeIsExpectedToHaveValueAndItDoesShouldNotThrow()
    {
        var maybe = Maybe.From("test");

        maybe.Should().HaveValue("test");
        maybe.Should().HaveValue("test").Which.Should().HaveLength(4);
    }

    [Fact]
    public void WhenMaybeIsExpectedToHaveValueAndItHasWrongValueShouldThrow()
    {
        var maybe = Maybe.From("oops");

        var act = () => maybe.Should().HaveValue("test", "it is test");

        act.Should().Throw<Exception>().WithMessage($"*value \"test\" because it is test, but with value \"oops\" it*");
    }

    [Fact]
    public void WhenMaybeIsExpectedToHaveValueAndItDoesNotShouldThrow()
    {
        Maybe<string> maybe = null;

        var act = () => maybe.Should().HaveValue("test", "it is not None");

        act.Should().Throw<Exception>().WithMessage($"*value \"test\" because it is not None*");
    }

    [Fact]
    public void WhenMaybeIsExpectedToHaveNoValueAndItHasNoneShouldNotThrow()
    {
        Maybe<string> maybe = null;

        maybe.Should().HaveNoValue();
    }

    [Fact]
    public void WhenMaybeIsExpectedToHaveNoValueAndItHasOneShouldThrow()
    {
        var maybe = Maybe.From("test");

        var act = () => maybe.Should().HaveNoValue("it is None");

        act.Should().Throw<Exception>().WithMessage($"*Maybe to have no value because it is None, but with value \"test\" it*");
    }
}
