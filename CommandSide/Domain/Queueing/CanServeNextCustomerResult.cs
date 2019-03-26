using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class CanServeNextCustomerResult : ValueObject<CanServeNextCustomerResult>
    {
        public static readonly CanServeNextCustomerResult CounterCantServeCustomer = new CanServeNextCustomerResult(nameof(CounterCantServeCustomer));
        public static readonly CanServeNextCustomerResult CounterCanServeCustomer = new CanServeNextCustomerResult(nameof(CounterCanServeCustomer));

        public static CanServeNextCustomerResult CounterCantServeCustomerBecauseOfError(string failureReason)
            => new CanServeNextCustomerResult(nameof(CounterCantServeCustomer), failureReason);

        public static CanServeNextCustomerResult CounterCanServeNextCustomerAndItIsCurrentlyServingCustomer(Maybe<Customer> maybeCurrentlyServingCustomer)
            => new CanServeNextCustomerResult(nameof(CounterCanServeCustomer), maybeCurrentlyServingCustomer);

        private readonly string _state;
        public string FailureReason { get; }
        public Maybe<Customer> MaybeCurrentlyServingCustomer { get; }

        private CanServeNextCustomerResult(string state, string failureReason = "")
            : this(state, Maybe<Customer>.None, failureReason)
        {
        }

        private CanServeNextCustomerResult(string state, Maybe<Customer> maybeCurrentlyServingCustomer, string failureReason = "")
        {
            FailureReason = failureReason;
            _state = state;
            MaybeCurrentlyServingCustomer = maybeCurrentlyServingCustomer;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _state;
        }

        public override string ToString() => _state;
    }
}