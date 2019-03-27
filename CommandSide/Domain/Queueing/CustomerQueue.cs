using System;
using Common;
using Shared.CustomerQueue.Events;
using static Common.Result;

namespace CommandSide.Domain.Queueing
{
    public sealed class CustomerQueue : AggregateRoot
    {
        public CustomerQueue(Guid id) : base(id)
        {
        }

        public static CustomerQueue NewCustomerQueueFrom(
            Guid id)
        {
            var customerQueue = new CustomerQueue(id);
            customerQueue.ApplyChange(new CustomerQueueCreated(customerQueue.Id));
            return customerQueue;
        }

        private CustomerQueue Apply(CustomerQueueCreated _) => this;
        
        private Maybe<CounterId> _addedCounter = Maybe<CounterId>.None;

        public Result<CustomerQueue> AddCounter(CounterId counterId)
        {
            if (_addedCounter.HasNoValue)
            {
                ApplyChange(new CounterAdded(Id, counterId));
                return Ok(this);
            }
            
                return Fail<CustomerQueue>($"Counter with id {counterId} already exists.");
        }

        private CustomerQueue Apply(CounterAdded e)
        {
            _addedCounter = e.CounterId.ToCounterId();
            return this;
        }
    }
}