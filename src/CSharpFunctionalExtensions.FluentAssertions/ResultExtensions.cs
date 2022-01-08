using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace CSharpFunctionalExtensions.FluentAssertions
{
    public static class ResultExtensions
    {
        public static ResultAssertions<T> Should<T>(this Result<T> instance) => new(instance);
    }

    public class ResultAssertions<T> : ReferenceTypeAssertions<Result<T>, ResultAssertions<T>>
    {
        public ResultAssertions(Result<T> instance) : base(instance) { }

        protected override string Identifier => "Result{T}";

        public AndConstraint<ResultAssertions<T>> BeSuccessful(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => Subject.IsSuccess)
                .ForCondition(isSuccess => isSuccess)
                .FailWith("Expected Result to be successful but found error");

            return new AndConstraint<ResultAssertions<T>>(this);
        }

        public AndConstraint<ResultAssertions<T>> BeSuccessfulWith(T value, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => Subject)
                .ForCondition(isSuccess => Subject.IsSuccess)
                .FailWith("Expected Result to be successful but found error")
                .Then
                .Given(s => s.Value)
                .ForCondition(v => v.Equals(value))
                .FailWith("Excepted Result value to be {0} but found {1}", value, Subject.Value);

            return new AndConstraint<ResultAssertions<T>>(this);
        }

        public AndConstraint<ResultAssertions<T>> BeFailure(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => Subject.IsFailure)
                .ForCondition(isSuccess => isSuccess)
                .FailWith("Expected Result to be failure");

            return new AndConstraint<ResultAssertions<T>>(this);
        }
    }
}
