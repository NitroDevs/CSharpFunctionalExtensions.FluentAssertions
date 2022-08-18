using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using FluentAssertions;

namespace CSharpFunctionalExtensions.FluentAssertions;
public static class ResultTEExtensions
{
    public static ResultTEAssertions<T, E> Should<T, E>(this Result<T, E> instance) => new(instance);
}

public class ResultTEAssertions<T, E> : ReferenceTypeAssertions<Result<T, E>, ResultTEAssertions<T, E>>
{
    public ResultTEAssertions(Result<T, E> instance) : base(instance) { }

    protected override string Identifier => "Result{T}";

    /// <summary>
    /// Asserts a result is a success.
    /// </summary>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<ResultTEAssertions<T, E>> Succeed(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject)
            .ForCondition(s => s.IsSuccess)
            .FailWith("Expected Result to be successful but it failed");

        return new AndConstraint<ResultTEAssertions<T, E>>(this);
    }

    /// <summary>
    /// Asserts a result is a success with a specified value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<ResultTEAssertions<T, E>> SucceedWith(T value, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject)
            .ForCondition(s => s.IsSuccess)
            .FailWith("Expected Result to be successful but it failed")
            .Then
            .Given(s => s.Value)
            .ForCondition(v => v!.Equals(value))
            .FailWith("Excepted Result value to be {0} but found {1}", value, Subject.Value);

        return new AndConstraint<ResultTEAssertions<T, E>>(this);
    }

    /// <summary>
    /// Asserts a result is a failure.
    /// </summary>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<ResultTEAssertions<T, E>> Fail(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject)
            .ForCondition(s => s.IsFailure)
            .FailWith("Expected Result to be failure but it succeeded");

        return new AndConstraint<ResultTEAssertions<T, E>>(this);
    }

    /// <summary>
    /// Asserts a result is a failure with a specified error.
    /// </summary>
    /// <param name="error"></param>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<ResultTEAssertions<T, E>> FailWith(E error, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject)
            .ForCondition(s => s.IsFailure)
            .FailWith("Expected Result to be failure but it succeeded")
            .Then
            .Given(s => s.Error)
            .ForCondition(e => e!.Equals(error))
            .FailWith("Excepted Result value to be {0} but found {1}", error, Subject.Error);

        return new AndConstraint<ResultTEAssertions<T, E>>(this);
    }
}
