﻿using CSharpFunctionalExtensions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace FluentAssertions;

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
    public AndWhichConstraint<ResultTAssertions<T>, T> Succeed(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject.IsSuccess)
            .FailWith(() => new FailReason(@$"Expected {{context:result}} to succeed{{reason}}, but it failed with error ""{Subject.Error}"""));

        return new AndWhichConstraint<ResultTAssertions<T>, T>(this, Subject.Value);
    }

    /// <summary>
    /// Asserts a result is a success with a specified value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndWhichConstraint<ResultTAssertions<T>, T> SucceedWith(T value, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject.IsSuccess)
            .FailWith(() => new FailReason(@$"Expected {{context:result}} to succeed{{reason}}, but it failed with error ""{Subject.Error}"""))
            .Then
            .Given(() => Subject.Value)
            .ForCondition(v => v!.Equals(value))
            .FailWith($"Expected {{context:result}} value to be {{0}}, but found {{1}}", value, Subject.Value);

        return new AndWhichConstraint<ResultTAssertions<T>, T>(this, Subject.Value);
    }

    /// <summary>
    /// Asserts a result is a failure.
    /// </summary>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndWhichConstraint<ResultTAssertions<T>, string> Fail(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject.IsFailure)
            .FailWith(() => new FailReason(@$"Expected {{context:result}} to fail, but it succeeded with value ""{Subject.Value}"""));

        return new AndWhichConstraint<ResultTAssertions<T>, string>(this, Subject.Error);
    }

    /// <summary>
    /// Asserts a result is a failure with a specified error.
    /// </summary>
    /// <param name="error"></param>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndWhichConstraint<ResultTAssertions<T>, string> FailWith(string error, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject.IsFailure)
            .ForCondition(b => Subject.Error!.Equals(error))
            .FailWith($"Expected {{context:result}} error to be {{0}}, but found {{1}}", error, Subject.Error);

        return new AndWhichConstraint<ResultTAssertions<T>, string>(this, Subject.Error);
    }
}
