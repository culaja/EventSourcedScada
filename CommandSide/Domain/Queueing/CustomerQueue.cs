using System;
using CommandSide.Domain.Queueing.Configuring;
using Common;
using Shared.CustomerQueue;
using static CommandSide.Domain.Queueing.CanOpenCounterResult;
using static CommandSide.Domain.Queueing.Configuring.OpenTimes;
using static CommandSide.Domain.Queueing.Counters;
using static Common.Result;

namespace CommandSide.Domain.Queueing
{
    public sealed class CustomerQueue : AggregateRoot
    {
        private Counters _counters = NoCounters;
        private OpenTimes _currentOpenTimes = NoOpenTimes;
        
        public CustomerQueue(Guid id) : base(id)
        {
        }
        
        public static CustomerQueue NewCustomerQueueFrom(
            Guid id)
        {
            var customerQueue = new CustomerQueue(id);
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
                    c.IsolateCountersToAdd(_counters.CountersDetails).Map(counterDetails =>
                        ApplyChange(new CounterAdded(Id, counterDetails.Id, counterDetails.Name)));

                    c.IsolateCounterIdsToRemove(_counters.CountersDetails).Map(counterId =>
                        ApplyChange(new CounterRemoved(Id, counterId)));

                    c.IsolateCountersDetailsWhereNameChanged(_counters.CountersDetails).Map( counterDetails =>
                        ApplyChange(new CounterNameChanged(Id, counterDetails.Id, counterDetails.Name)));
                    
                    c.IsolateOpenTimesToAdd(_currentOpenTimes).Map(openTime =>
                        ApplyChange(new OpenTimeAdded(Id, openTime.Day, openTime.BeginTimestamp, openTime.EndTimestamp)));

                    c.IsolateOpenTimesToRemove(_currentOpenTimes).Map(openTime =>
                        ApplyChange(new OpenTimeRemoved(Id, openTime.Day, openTime.BeginTimestamp, openTime.EndTimestamp)));


                    return Ok(this);
                });

        private CustomerQueue Apply(CounterAdded e)
        {
            _counters = _counters.AddCounterWith(e.CounterId.ToCounterId(), e.CounterName.ToCounterName());
            return this;
        }

        private CustomerQueue Apply(CounterRemoved e)
        {
            _counters = _counters.Remove(e.CounterId.ToCounterId());
            return this;
        }

        private CustomerQueue Apply(CounterNameChanged e)
        {
            _counters.ChangeCounterName(e.CounterId.ToCounterId(), e.NewCounterName.ToCounterName());
            return this;
        }

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

        public Result<CustomerQueue> OpenCounter(CounterId counterId)
        {
            switch (_counters.CanOpenCounter(counterId))
            {
                case var s when s == CounterCanBeOpened :
                    ApplyChange(new CounterOpened(Id, counterId));
                    return Ok(this);
                case var s when s == CounterIsAlreadyOpened:
                    return Ok(this);
                case var s when s == CounterDoesntExist:
                    return Fail<CustomerQueue>($"Counter with ID {counterId} doesn't exist.");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private CustomerQueue Apply(CounterOpened e)
        {
            _counters.OpenCounterWith(e.CounterId.ToCounterId());
            return this;
        }

        public Result<CustomerQueue> CloseCounter(CounterId counterId)
        {
            switch (_counters.CanCloseCounter(counterId))
            {
                case var s when s == CanCloseCounterResult.CounterCanBeClosed :
                    ApplyChange(new CounterClosed(Id, counterId));
                    return Ok(this);
                case var s when s == CanCloseCounterResult.CounterIsAlreadyClosed:
                    return Ok(this);
                case var s when s == CanCloseCounterResult.CounterDoesntExist:
                    return Fail<CustomerQueue>($"Counter with ID {counterId} doesn't exist.");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private CustomerQueue Apply(CounterClosed e)
        {
            _counters.CloseCounterWith(e.CounterId.ToCounterId());
            return this;
        }
    }
}