using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class CanServeNextCustomerResult : ValueObject<CanServeNextCustomerResult>
    {
        public static readonly CanServeNextCustomerResult CounterDoesntExist = new CanServeNextCustomerResult(nameof(CounterDoesntExist));
        public static readonly CanServeNextCustomerResult CounterCantServeCustomer = new CanServeNextCustomerResult(nameof(CounterCantServeCustomer));
        public static readonly CanServeNextCustomerResult CounterCanServeCustomer = new CanServeNextCustomerResult(nameof(CounterCanServeCustomer));
        
        public static CanServeNextCustomerResult CounterCantBeServedBecauseOfError(string errorMessage) 
            => new CanServeNextCustomerResult(nameof(CounterCantServeCustomer), errorMessage);
        
        public static CanServeNextCustomerResult CounterCanBeServedWithNextCustomerAndItIsCurrentlyServingCustomer(Maybe<Customer> maybeCurrentlyServingCustomer) 
            => new CanServeNextCustomerResult(nameof(CounterCanServeCustomer), maybeCurrentlyServingCustomer);
        
        private readonly string _state;
        public string FailureReason { get; }
        public Maybe<Customer> MaybeCurrentlyServingCustomer { get; }
        
        private CanServeNextCustomerResult(string state, string errorMessage = "")
            :this(state, Maybe<Customer>.None, errorMessage)
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