using System;
using System.Collections.Generic;
using Common;
using Shared.CustomerQueue;
using static Common.Result;

namespace Domain
{
    public sealed class CustomerQueue : AggregateRoot
    {
        private readonly List<Counter> _counters;
        public IReadOnlyList<Counter> Counters => _counters;
        
        public CustomerQueue(
            Guid id,
            ulong version,
            IReadOnlyList<Counter> counters) : base(id, version)
        {
            _counters = new List<Counter>(counters);
        }

        public static CustomerQueue NewCustomerQueueFrom(
            Guid id)
        {
            var customerQueue = new CustomerQueue(
                id,
                0,
                new List<Counter>());
            customerQueue.ApplyChange(new CustomerQueueCreated(
                customerQueue.Id));
            return customerQueue;
        }

        private CustomerQueue Apply(CustomerQueueCreated _) => this;

        public Result<CustomerQueue> AddCounter(Guid counterId, string counterName)
        {
            ApplyChange(new CounterAdded(Id, counterId, counterName));
            return Ok<CustomerQueue>(this);
        }

        private CustomerQueue Apply(CounterAdded e)
        {
            _counters.Add(new Counter(e.CounterId, e.CounterName));
            return this;
        }
    }
}