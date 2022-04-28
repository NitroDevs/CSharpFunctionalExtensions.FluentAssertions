using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace CSharpFunctionalExtensions.FluentAssertions;

public static class ResultTExtensions
{
    public static ResultTAssertions<T> Should<T>(this Result<T> instance) => new(instance);
}

public class ResultTAssertions<T> : ReferenceTypeAssertions<Result<T>, ResultTAssertions<T>>
{
    public ResultTAssertions(Result<T> instance) : base(instance) { }

    protected override string Identifier => "Result{T}";

    /// <summary>
    /// Asserts a result is a success.
    /// </summary>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<ResultTAssertions<T>> Succeed(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject.IsSuccess)
            .ForCondition(isSuccess => isSuccess)
            .FailWith("Expected Result to be successful but it failed");

        return new AndConstraint<ResultTAssertions<T>>(this);
    }

    /// <summary>
    /// Asserts a result is a failure.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<ResultTAssertions<T>> SucceedWith(T value, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject)
            .ForCondition(isSuccess => Subject.IsSuccess)
            .FailWith("Expected Result to be successful but it failed")
            .Then
            .Given(s => s.Value)
            .ForCondition(v => v.Equals(value))
            .FailWith("Excepted Result value to be {0} but found {1}", value, Subject.Value);

        return new AndConstraint<ResultTAssertions<T>>(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<ResultTAssertions<T>> Fail(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject.IsFailure)
            .ForCondition(isSuccess => isSuccess)
            .FailWith("Expected Result to be failure but it succeeded");

        return new AndConstraint<ResultTAssertions<T>>(this);
    }
}
