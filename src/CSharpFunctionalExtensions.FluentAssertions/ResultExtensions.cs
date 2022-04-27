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
    /// 
    /// </summary>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<ResultAssertions> BeSuccessful(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject.IsSuccess)
            .ForCondition(isSuccess => isSuccess)
            .FailWith("Expected Result to be successful but found error");

        return new AndConstraint<ResultAssertions>(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndConstraint<ResultAssertions> BeFailure(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject.IsFailure)
            .ForCondition(isSuccess => isSuccess)
            .FailWith("Expected Result to be failure but found success");

        return new AndConstraint<ResultAssertions>(this);
    }
}
