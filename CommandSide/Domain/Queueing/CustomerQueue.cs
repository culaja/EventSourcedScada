using System;
using System.Collections.Generic;
using System.Linq;
using CommandSide.Domain.Queueing.Configuring;
using Common;
using Shared.CustomerQueue;
using static CommandSide.Domain.Queueing.Configuring.OpenTimes;
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

        public Result<CustomerQueue> SetConfiguration(Configuration c) =>
            c.ContainsOverlappingOpenTimeWith(_currentOpenTimes).OnBoth(
                () => Fail<CustomerQueue>($"One of the open times in {c.OpenTimes} is overlapping with an open time in {_currentOpenTimes}."),
                () =>
                {
                    c.IsolateCountersToAdd(_countersAdded).Map(counterDetails =>
                        ApplyChange(new CounterAdded(Id, counterDetails.Id, counterDetails.Name)));

                    c.IsolateCounterIdsToRemove(_countersAdded).Map(counterId =>
                        ApplyChange(new CounterRemoved(Id, counterId)));

                    c.IsolateOpenTimesToAdd(_currentOpenTimes).Map(openTime =>
                        ApplyChange(new OpenTimeAdded(Id, openTime.Day, openTime.BeginTimestamp, openTime.EndTimestamp)));

                    c.IsolateOpenTimesToRemove(_currentOpenTimes).Map(openTime =>
                        ApplyChange(new OpenTimeRemoved(Id, openTime.Day, openTime.BeginTimestamp, openTime.EndTimestamp)));
                    
                    return Ok(this);
                });

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
        
        private OpenTimes _currentOpenTimes = NoOpenTimes;

        private CustomerQueue Apply(OpenTimeAdded e)
        {
            _currentOpenTimes = _currentOpenTimes.Add(e.ToOpenTime());
            return this;
        }

        private CustomerQueue Apply(OpenTimeRemoved e)
        {
            _currentOpenTimes = _currentOpenTimes.Remove(e.ToOpenTime());
            return this;
        }
    }
}