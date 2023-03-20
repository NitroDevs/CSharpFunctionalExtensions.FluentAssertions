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
            .ForCondition(Subject.IsSuccess)
            .FailWith(() => new FailReason(@$"Expected {{context:result}} to be successful{{reason}} but it failed with error ""{Subject.Error}"""));

        return new AndConstraint<ResultTAssertions<T>>(this);
    }

    /// <summary>
    /// Asserts a result is a success with a specified value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<ResultTAssertions<T>> SucceedWith(T value, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject.IsSuccess)
            .FailWith(() => new FailReason(@$"Expected {{context:result}} to be successful{{reason}} but it failed with error ""{Subject.Error}"""))
            .Then
            .Given(() => Subject.Value)
            .ForCondition(v => v!.Equals(value))
            .FailWith("Expected Result value to be {0} but found {1}", value, Subject.Value);

        return new AndConstraint<ResultTAssertions<T>>(this);
    }

    /// <summary>
    /// Asserts a result is a failure
    /// </summary>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<ResultTAssertions<T>> Fail(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject.IsFailure)
            .FailWith(() => new FailReason($"Expected {{context:result}} to be failure but it succeeded"));

        return new AndConstraint<ResultTAssertions<T>>(this);
    }
}
