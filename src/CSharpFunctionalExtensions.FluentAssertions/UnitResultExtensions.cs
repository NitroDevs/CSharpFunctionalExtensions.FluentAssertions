using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using FluentAssertions;

namespace CSharpFunctionalExtensions.FluentAssertions;
public static class UnitResultExtensions
{
    public static UnitResultAssertions<E> Should<E>(this UnitResult<E> instance) => new(instance);
}

public class UnitResultAssertions<E> : ReferenceTypeAssertions<UnitResult<E>, UnitResultAssertions<E>>
{
    public UnitResultAssertions(UnitResult<E> instance) : base(instance) { }

    protected override string Identifier => "Result";

    /// <summary>
    /// Asserts a unit result is a success.
    /// </summary>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<UnitResultAssertions<E>> Succeed(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject)
            .ForCondition(s => s.IsSuccess)
            .FailWith("Expected UnitResult to be successful but it failed");

        return new AndConstraint<UnitResultAssertions<E>>(this);
    }

    /// <summary>
    /// Asserts a unit result is a failure.
    /// </summary>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<UnitResultAssertions<E>> Fail(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject)
            .ForCondition(s => s.IsFailure)
            .FailWith("Expected UnitResult to be failure but it succeeded");

        return new AndConstraint<UnitResultAssertions<E>>(this);
    }

    /// <summary>
    /// Asserts a unit result is a failure with a specfied error.
    /// </summary>
    /// <param name="error"></param>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<UnitResultAssertions<E>> Fail(E error, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject)
            .ForCondition(s => s.IsFailure)
            .FailWith("Expected UnitResult to be failure but it succeeded")
            .Then
            .Given(s => s.Error)
            .ForCondition(e => e.Equals(error))
            .FailWith("Excepted UnitResult value to be {0} but found {1}", error, Subject.Error);

        return new AndConstraint<UnitResultAssertions<E>>(this);
    }
}
