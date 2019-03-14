using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class CanServeNextCustomerResult : ValueObject<CanServeNextCustomerResult>
    {
        public static readonly CanServeNextCustomerResult CounterDoesntExist = new CanServeNextCustomerResult(nameof(CounterDoesntExist));
        public static readonly CanServeNextCustomerResult CounterCantBeServed = new CanServeNextCustomerResult(nameof(CounterCantBeServed));
        public static readonly CanServeNextCustomerResult CounterCanServeCustomer = new CanServeNextCustomerResult(nameof(CounterCanServeCustomer));
        
        public static CanServeNextCustomerResult CounterCantBeServedBecauseOfError(string errorMessage) 
            => new CanServeNextCustomerResult(nameof(CounterCantBeServed), errorMessage);
        
        public static CanServeNextCustomerResult CounterCanBeServedWithNextCustomerAndItIsCurrentlyServingCustomer(Maybe<TicketId> maybeCurrentlyServingCustomer) 
            => new CanServeNextCustomerResult(nameof(CounterCanServeCustomer), maybeCurrentlyServingCustomer);
        
        private readonly string _state;
        public string ErrorMessage { get; }
        public Maybe<TicketId> MaybeCurrentlyServingCustomer { get; }
        
        private CanServeNextCustomerResult(string state, string errorMessage = "")
            :this(state, Maybe<TicketId>.None, errorMessage)
        {
        }

        private CanServeNextCustomerResult(string state, Maybe<TicketId> maybeCurrentlyServingCustomer, string errorMessage = "")
        {
            ErrorMessage = errorMessage;
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