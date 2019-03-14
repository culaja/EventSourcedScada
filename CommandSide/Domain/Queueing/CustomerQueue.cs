using System;
using System.Collections.Generic;
using CommandSide.Domain.Queueing.Configuring;
using Common;
using Shared.CustomerQueue;
using static CommandSide.Domain.Queueing.CanOpenCounterResult;
using static CommandSide.Domain.Queueing.CanServeNextCustomerResult;
using static CommandSide.Domain.Queueing.Counters;
using static Common.Result;

namespace CommandSide.Domain.Queueing
{
    public sealed class CustomerQueue : AggregateRoot
    {
        private Counters _counters = NoCounters;
        private readonly Queue<TicketId> _customerQueue = new Queue<TicketId>();
        
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
                case var s when s == CanOpenCounterResult.CounterDoesntExist:
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

        public Result<CustomerQueue> EnqueueCustomerWith(TicketId ticketId)
        {
            ApplyChange(new CustomerEnqueued(Id, ticketId));
            return Ok(this);
        }

        private CustomerQueue Apply(CustomerEnqueued e)
        {
            _customerQueue.Enqueue(e.TicketId.ToTicketId());
            return this;
        }

        public Result<CustomerQueue> NextCustomer(CounterId counterId)
        {
            switch (_counters.CanServeNextCustomer(counterId))
            {
                case var s when s == CanServeNextCustomerResult.CounterDoesntExist :
                    return Fail<CustomerQueue>($"Counter with ID '{nameof(counterId)}' doesn't exist.");
                case var s when s == CounterCantBeServed:
                    return Fail<CustomerQueue>(s.ErrorMessage);
                case var s when s == CounterCanServeCustomer:
                    s.MaybeCurrentlyServingCustomer.Map(ticketId => ApplyChange(new CustomerServedByCounter(Id, ticketId, counterId)));
                    _customerQueue.MaybeFirst().Map(ticketId => ApplyChange(new CustomerAssignedToCounter(Id, ticketId, counterId)));
                    return Ok(this);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private CustomerQueue Apply(CustomerAssignedToCounter e)
        {
            _counters.AssignCustomerToCounter(e.CounterId.ToCounterId(), _customerQueue.Dequeue());
            return this;
        }

        private CustomerQueue Apply(CustomerServedByCounter e)
        {
            _counters.UnassignCustomerFromCounter(e.CounterId.ToCounterId(), e.TicketId.ToTicketId());
            return this;
        }
    }
}