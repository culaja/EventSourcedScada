using System;
using System.Collections.Generic;
using System.Linq;
using CommandSide.Domain.Queueing.Configuring;
using Common;
using Shared.CustomerQueue;
using static Common.Result;

namespace CommandSide.Domain.Queueing
{
    public sealed class CustomerQueue : AggregateRoot
    {
        public CustomerQueue(
            Guid id,
            ulong version) : base(id, version)
        {
        }
        
        public static CustomerQueue NewCustomerQueueFrom(
            Guid id)
        {
            var customerQueue = new CustomerQueue(
                id,
                0);
            customerQueue.ApplyChange(new CustomerQueueCreated(
                customerQueue.Id));
            return customerQueue;
        }

        private CustomerQueue Apply(CustomerQueueCreated _) => this;

        public Result<CustomerQueue> SetConfiguration(Configuration c)
        {
            c.IsolateCountersToAdd(_countersAdded).Map(counterDetails => 
                ApplyChange(new CounterAdded(Id, counterDetails.Id, counterDetails.Name)));

            c.IsolateCounterIdsToRemove(_countersAdded).Map(counterId =>
                ApplyChange(new CounterRemoved(Id, counterId)));
            
            foreach (var openTime in c.OpenTimes)
            {
                ApplyChange(new OpenTimeAdded(Id, openTime.Day, openTime.BeginTimestamp, openTime.EndTimestamp));
            }

            foreach (var openTime in _openTimesAdded)
            {
                ApplyChange(new OpenTimeRemoved(Id, openTime.Day, openTime.BeginTimestamp, openTime.EndTimestamp));
            }
            
            return Ok(this);
        }

        private readonly List<CounterId> _countersAdded = new List<CounterId>();

        private CustomerQueue Apply(CounterAdded e)
        {
            _countersAdded.Add(e.CounterId.ToCounterId());
            return this;
        }

        private CustomerQueue Apply(CounterRemoved e)
        {
            _countersAdded.Remove(e.CounterId.ToCounterId());
            return this;
        }
        
        private readonly List<OpenTime> _openTimesAdded = new List<OpenTime>();

        private CustomerQueue Apply(OpenTimeAdded e)
        {
            _openTimesAdded.Add(e.ToOpenTime());
            return this;
        }

        private CustomerQueue Apply(OpenTimeRemoved e)
        {
            return this;
        }
    }
}