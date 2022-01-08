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
                .FailWith("Expected Result to be successful but found error {Subject.Error}", Subject.Error);

            return new AndConstraint<ResultAssertions<T>>(this);
        }
    }
}
