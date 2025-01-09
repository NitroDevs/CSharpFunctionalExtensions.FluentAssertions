﻿using CSharpFunctionalExtensions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace FluentAssertions;
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
    public AndWhichConstraint<ResultTEAssertions<T, E>, T> Succeed(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject.IsSuccess)
            .FailWith(() => new FailReason(@$"Expected {{context:result}} to succeed{{reason}}, but it failed with error ""{Subject.Error}"""));

        return new AndWhichConstraint<ResultTEAssertions<T, E>, T>(this, Subject.Value);
    }

    /// <summary>
    /// Asserts a result is a success with a specified value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndWhichConstraint<ResultTEAssertions<T, E>, T> SucceedWith(T value, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject.IsSuccess)
            .FailWith(() => new FailReason(@$"Expected {{context:result}} to succeed{{reason}}, but it failed with error ""{Subject.Error}"""))
            .Then
            .Given(() => Subject.Value)
            .ForCondition(v => v!.Equals(value))
            .FailWith("Expected {context:result} value to be {0}, but found {1}", value, Subject.Value);

        return new AndWhichConstraint<ResultTEAssertions<T, E>, T>(this, Subject.Value);
    }

    /// <summary>
    /// Asserts a result is a failure.
    /// </summary>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndWhichConstraint<ResultTEAssertions<T, E>, E> Fail(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject.IsFailure)
            .FailWith(() => new FailReason(@$"Expected {{context:result}} to fail, but it succeeded with value ""{Subject.Value}"""));

        return new AndWhichConstraint<ResultTEAssertions<T, E>, E>(this, Subject.Error);
    }

    /// <summary>
    /// Asserts a result is a failure with a specified error.
    /// </summary>
    /// <param name="error"></param>
    /// <param name="because"></param>
    /// <param name="becauseArgs"></param>
    /// <returns></returns>
    public AndWhichConstraint<ResultTEAssertions<T, E>, E> FailWith(E error, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject.IsFailure)
            .FailWith(() => new FailReason($"Expected {{context:result}} to fail, but it succeeded"))
            .Then
            .Given(() => Subject.Error)
            .ForCondition(e => e!.Equals(error))
            .FailWith($"Expected {{context:result}} error to be {{0}}, but found {{1}}", error, Subject.Error);

        return new AndWhichConstraint<ResultTEAssertions<T, E>, E>(this, Subject.Error);
    }
}
