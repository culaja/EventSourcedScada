using System;
using System.Collections.Generic;
using System.Linq;
using CommandSide.Domain.Queueing.Configuring;
using Common;
using Shared.CustomerQueue;
using static CommandSide.Domain.Queueing.CanCloseCounterResult;
using static CommandSide.Domain.Queueing.CanOpenCounterResult;
using static CommandSide.Domain.Queueing.CanRecallCustomerResult;
using static CommandSide.Domain.Queueing.CanServeNextCustomerResult;
using static CommandSide.Domain.Queueing.Counters;
using static CommandSide.Domain.Queueing.Customer;
using static Common.Result;

namespace CommandSide.Domain.Queueing
{
    public sealed class CustomerQueue : AggregateRoot
    {
        private Counters _counters = NoCounters;
        private readonly Queue<Customer> _queue = new Queue<Customer>();
        
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
                case var s when s == CounterCantBeOpened:
                    return Fail<CustomerQueue>(s.FailureReason);
                case var s when s == CounterCanBeOpened :
                    ApplyChange(new CounterOpened(Id, counterId));
                    return Ok(this);
                case var s when s == CounterIsAlreadyOpened:
                    return Ok(this);
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
                case var s when s == CounterCantBeClosed:
                    return Fail<CustomerQueue>(s.FailureReason);
                case var s when s == CounterCanBeClosed :
                    ApplyChange(new CounterClosed(Id, counterId));
                    return Ok(this);
                case var s when s == CounterIsAlreadyClosed:
                    return Ok(this);
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
            _queue.Enqueue(NewCustomerFrom(e.TicketId.ToTicketId()));
            return this;
        }

        public Result<CustomerQueue> NextCustomer(CounterId counterId)
        {
            switch (_counters.CanServeACustomer(counterId))
            {
                case var s when s == CounterCantServeCustomer:
                    return Fail<CustomerQueue>(s.FailureReason);
                case var s when s == CounterCanServeCustomer:
                    s.MaybeCurrentlyServingCustomer.Map(customer => ApplyChange(new CustomerServedByCounter(Id, customer.Id, counterId)));
                    _queue.MaybeFirst().Map(customer => ApplyChange(new CustomerAssignedToCounter(Id, customer.Id, counterId)));
                    return Ok(this);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private CustomerQueue Apply(CustomerAssignedToCounter e)
        {
            _counters.AssignCustomerToCounter(e.CounterId.ToCounterId(), _queue.Dequeue());
            return this;
        }

        private CustomerQueue Apply(CustomerServedByCounter e)
        {
            _counters.UnassignCustomerFromCounter(e.CounterId.ToCounterId());
            return this;
        }

        public Result<CustomerQueue> ReCallCustomerFor(CounterId counterId)
        {
            switch (_counters.CanRecallCustomer(counterId))
            {
                case var s when s == CounterCantRecallCustomer:
                    return Fail<CustomerQueue>(s.FailureReason);
                case var s when s == CounterCanRecallCustomer:
                    ApplyChange(new CustomerRecalledByCounter(Id, s.AssignedCustomer.Id, counterId));
                    return Ok(this);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private CustomerQueue Apply(CustomerRecalledByCounter _) => this;

        public Result<CustomerQueue> RemoveWaitingCustomers()
        {
            _queue.CanEmptyQueue(() => ApplyChange(new WaitingCustomersRemoved(Id, _queue.AllTicketIds())));
            return Ok(this);
        }

        private CustomerQueue Apply(WaitingCustomersRemoved _)
        {
            _queue.Clear();
            return this;
        }

        public Result<CustomerQueue> ServeOutOfLineCustomer(CounterId counterId, TicketId ticketId)
        {
            switch (_counters.CanServeACustomer(counterId))
            {
                case var s when s == CounterCantServeCustomer:
                    return Fail<CustomerQueue>(s.FailureReason);
                case var s when s == CounterCanServeCustomer:
                    s.MaybeCurrentlyServingCustomer.Map(customer => ApplyChange(new CustomerServedByCounter(Id, customer.Id, counterId)));
                    ApplyChange(new OutOfLineCustomerAssignedToCounter(Id, ticketId, counterId));
                    return Ok(this);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private CustomerQueue Apply(OutOfLineCustomerAssignedToCounter e)
        {
            _counters.AssignCustomerToCounter(e.CounterId.ToCounterId(), NewCustomerFrom(e.TicketId.ToTicketId()));
            return this;
        }
    }
}