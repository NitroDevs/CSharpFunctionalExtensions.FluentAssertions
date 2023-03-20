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
            .ForCondition(Subject.IsSuccess)
            .FailWith(() => new FailReason(@$"Expected {{context:result}} to be successful{{reason}} but it failed with error ""{Subject.Error}"""));

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
            .ForCondition(Subject.IsFailure)
            .FailWith(() => new FailReason($"Expected {{context:result}} to be failure{{reason}} but it succeeded"));

        return new AndConstraint<ResultAssertions>(this);
    }
}
