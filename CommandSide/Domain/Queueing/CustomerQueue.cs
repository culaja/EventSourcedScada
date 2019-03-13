using System;
using CommandSide.Domain.Queueing.Configuring;
using CommandSide.Domain.TicketIssuing;
using Common;
using Shared.CustomerQueue;
using static CommandSide.Domain.Queueing.CanOpenCounterResult;
using static CommandSide.Domain.TicketIssuing.OpenTimes;
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
            customerQueue.ApplyChange(new CustomerQueueCreated(customerQueue.Id));
            return customerQueue;
        }

        private CustomerQueue Apply(CustomerQueueCreated _) => this;

        public Result<CustomerQueue> SetCounterConfiguration(CounterConfiguration counterConfiguration)
        {
            counterConfiguration.IsolateCounterIdsToRemove(_counters.CounterConfiguration).Map(counterId =>
                ApplyChange(new CounterRemoved(Id, counterId)));
            
            counterConfiguration.IsolateCountersToAdd(_counters.CounterConfiguration).Map(counterDetails =>
                ApplyChange(new CounterAdded(Id, counterDetails.Id, counterDetails.Name)));

            counterConfiguration.IsolateCountersDetailsWhereNameDiffers(_counters.CounterConfiguration).Map( counterDetails =>
                ApplyChange(new CounterNameChanged(Id, counterDetails.Id, counterDetails.Name)));
            
            return Ok(this);
        }

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

        public Result<CustomerQueue> EnqueueCustomer()
        {
            ApplyChange(new CustomerEnqueued(Id));
            return Ok(this);
        }

        private CustomerQueue Apply(CustomerEnqueued _) => this;
    }
}