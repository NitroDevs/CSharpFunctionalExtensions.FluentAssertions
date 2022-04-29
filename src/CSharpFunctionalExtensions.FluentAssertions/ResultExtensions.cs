using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace CSharpFunctionalExtensions.FluentAssertions;

public static class ResultExtensions
{
    public static ResultAssertions Should(this Result instance) => new(instance);
}

public class ResultAssertions : ReferenceTypeAssertions<Result, ResultAssertions>
{
    public ResultAssertions(Result instance) : base(instance) { }

    protected override string Identifier => "Result";

    /// <summary>
    /// Asserts a result is a success.
    /// </summary>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<ResultAssertions> Succeed(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject.IsSuccess)
            .ForCondition(isSuccess => isSuccess)
            .FailWith("Expected Result to be successful but it failed");

        return new AndConstraint<ResultAssertions>(this);
    }

    /// <summary>
    /// Asserts a result is a failure.
    /// </summary>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<ResultAssertions> Fail(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject.IsFailure)
            .ForCondition(isSuccess => isSuccess)
            .FailWith("Expected Result to be failure but it succeeded");

        return new AndConstraint<ResultAssertions>(this);
    }
}
