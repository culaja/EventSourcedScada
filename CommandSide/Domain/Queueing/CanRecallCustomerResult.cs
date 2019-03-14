using System;
using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class CanRecallCustomerResult : ValueObject<CanRecallCustomerResult>
    {
        public static readonly CanRecallCustomerResult CounterCantRecallCustomer = new CanRecallCustomerResult(nameof(CounterCantRecallCustomer));
        public static readonly CanRecallCustomerResult CounterCanRecallCustomer = new CanRecallCustomerResult(nameof(CounterCanRecallCustomer));
        
        public static CanRecallCustomerResult CounterCantBeRecalledBecauseOfError(string failureReason) 
            => new CanRecallCustomerResult(nameof(CounterCantRecallCustomer), failureReason);
        
        public static CanRecallCustomerResult CounterCanRecallCustomerFrom(Customer customer) => new CanRecallCustomerResult(nameof(CounterCanRecallCustomer), customer);
        
        private readonly string _state;
        private readonly Maybe<Customer> _maybeCustomer;
        public string FailureReason { get; }

        public Customer AssignedCustomer => _maybeCustomer.Unwrap(
            customer => customer,
            () => throw new InvalidOperationException($"Customer is only available when '{nameof(CounterCanRecallCustomer)}'"));
        
        private CanRecallCustomerResult(string state, Maybe<Customer> maybeCustomer, string failureReason = "")
        {
            _state = state;
            _maybeCustomer = maybeCustomer;
            FailureReason = failureReason;
        }
        
        private CanRecallCustomerResult(string state, Customer customer)
            : this(state, Maybe<Customer>.From(customer))
        {
        }
        
        private CanRecallCustomerResult(string state, string failureReason = "")
            : this(state, Maybe<Customer>.None, failureReason)
        {
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _state;
        }

        public override string ToString() => _state;
    }
}