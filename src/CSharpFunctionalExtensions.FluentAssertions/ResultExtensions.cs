using CSharpFunctionalExtensions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace FluentAssertions;

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
            .FailWith(() => new FailReason(@$"Expected {{context:result}} to succeed{{reason}}, but it failed with error ""{Subject.Error}"""));

        return new AndConstraint<ResultAssertions>(this);
    }

    /// <summary>
    /// Asserts a result is a failure.
    /// </summary>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndWhichConstraint<ResultAssertions, string> Fail(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject.IsFailure)
            .FailWith(() => new FailReason($"Expected {{context:result}} to fail{{reason}}, but it succeeded"));

        return new AndWhichConstraint<ResultAssertions, string>(this, Subject.Error);
    }

    /// <summary>
    /// Asserts a result is a failure with a specified error.
    /// </summary>
    /// <param name="error"></param>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndWhichConstraint<ResultAssertions, string> FailWith(string error, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject.IsFailure)
            .ForCondition(b => Subject.Error!.Equals(error))
            .FailWith($"Expected {{context:result}} error to be {{0}}, but found {{1}}", error, Subject.Error);

        return new AndWhichConstraint<ResultAssertions, string>(this, Subject.Error);
    }
}
