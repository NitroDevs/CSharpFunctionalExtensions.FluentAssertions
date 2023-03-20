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
            .ForCondition(Subject.IsSuccess)
            .FailWith(() => new FailReason(@$"Expected {{context:result}} to succeed{{reason}}, but it failed with error ""{Subject.Error}"""));

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
            .ForCondition(Subject.IsFailure)
            .FailWith(() => new FailReason($"Expected {{context:result}} to fail, but it succeeded"));

        return new AndConstraint<UnitResultAssertions<E>>(this);
    }

    /// <summary>
    /// Asserts a unit result is a failure with a specified error.
    /// </summary>
    /// <param name="error"></param>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<UnitResultAssertions<E>> FailWith(E error, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject.IsFailure)
            .FailWith(() => new FailReason($"Expected {{context:result}} to fail, but it succeeded"))
            .Then
            .Given(() => Subject.Error)
            .ForCondition(e => e!.Equals(error))
            .FailWith($"Expected {{context:result}} error to be {{0}}, but found {{1}}", error, Subject.Error);

        return new AndConstraint<UnitResultAssertions<E>>(this);
    }
}
