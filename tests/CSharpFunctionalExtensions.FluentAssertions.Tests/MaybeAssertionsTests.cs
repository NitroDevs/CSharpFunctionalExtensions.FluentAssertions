using System;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.FluentAssertions.Tests;

public class MaybeAssertionsTests
{
    [Fact]
    public void WhenMaybeIsExpectedToHaveValueAndItDoesShouldNotThrow()
    {
        var x = Maybe.From("test");

        x.Should().HaveValue("test");
    }

    [Fact]
    public void WhenMaybeIsExpectedToHaveValueAndItHasWrongValueShouldThrow()
    {
        var x = Maybe.From("oops");

        Action act = () => x.Should().HaveValue("test", "it is test");

        act.Should().Throw<Exception>().WithMessage($"*value \"test\" because it is test, but with value \"oops\" it*");
    }

    [Fact]
    public void WhenMaybeIsExpectedToHaveValueAndItDoesNotShouldThrow()
    {
        Maybe<string> x = null;

        Action act = () => x.Should().HaveValue("test", "it is not None");

        act.Should().Throw<Exception>().WithMessage($"*value \"test\" because it is not None*");
    }

    [Fact]
    public void WhenMaybeIsExpectedToHaveNoValueAndItHasNoneShouldNotThrow()
    {
        Maybe<string> x = null;

        x.Should().HaveNoValue();
    }

    [Fact]
    public void WhenMaybeIsExpectedToHaveNoValueAndItHasOneShouldThrow()
    {
        var x = Maybe.From("test");

        Action act = () => x.Should().HaveNoValue("it is None");

        act.Should().Throw<Exception>().WithMessage($"*Maybe to have no value because it is None, but with value \"test\" it*");
    }
}
