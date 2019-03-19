using System;
using System.Collections.Generic;
using System.Linq;
using CommandSide.Domain;
using CommandSide.Domain.Queueing;
using Shared.CustomerQueue;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueConfigurationTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications
{
    public static class CustomerQueueTestValues
    {
        public static readonly Guid SingleCustomerQueueId = Guid.NewGuid();
        
        public static readonly CustomerQueueCreated SingleCustomerQueueCreated = new CustomerQueueCreated(SingleCustomerQueueId);
        
        public static readonly CounterAdded Counter1Added = new CounterAdded(SingleCustomerQueueId, Counter1Id, Counter1Name);
        public static readonly CounterAdded Counter2Added = new CounterAdded(SingleCustomerQueueId, Counter2Id, Counter2Name);
        public static readonly CounterAdded Counter3Added = new CounterAdded(SingleCustomerQueueId, Counter3Id, Counter3Name);
        public static readonly IReadOnlyList<CounterAdded> AllCountersAdded = new[] {Counter1Added, Counter2Added, Counter3Added};
        public static readonly IReadOnlyList<CounterAdded> FirstTwoCountersAdded = new[] {Counter1Added, Counter2Added};
        
        public static readonly CounterRemoved Counter1Removed = new CounterRemoved(SingleCustomerQueueId, Counter1Id);
        public static readonly CounterRemoved Counter2Removed = new CounterRemoved(SingleCustomerQueueId, Counter2Id);
        public static readonly CounterRemoved Counter3Removed = new CounterRemoved(SingleCustomerQueueId, Counter3Id);
        public static readonly IReadOnlyList<CounterRemoved> AllCountersRemoved = new[] {Counter1Removed, Counter2Removed, Counter3Removed};
        
        public static readonly CounterNameChanged Counter1NameChanged = new CounterNameChanged(SingleCustomerQueueId, Counter1Id, Counter1ChangedName);
        public static readonly CounterNameChanged Counter2NameChanged = new CounterNameChanged(SingleCustomerQueueId, Counter2Id, Counter2ChangedName);
        public static readonly CounterNameChanged Counter3NameChanged = new CounterNameChanged(SingleCustomerQueueId, Counter3Id, Counter3ChangedName);
        public static readonly IReadOnlyList<CounterNameChanged> AllCountersNamesChanged = new[] {Counter1NameChanged, Counter2NameChanged, Counter3NameChanged};
        
        public static readonly CounterOpened Counter1Opened = new CounterOpened(SingleCustomerQueueId, Counter1Id);
        
        public static readonly CounterClosed Counter1Closed = new CounterClosed(SingleCustomerQueueId, Counter1Id);
        
        public static readonly CustomerEnqueued Customer1Enqueued = new CustomerEnqueued(SingleCustomerQueueId, Customer1TicketId);
        public static readonly CustomerEnqueued Customer2Enqueued = new CustomerEnqueued(SingleCustomerQueueId, Customer2TicketId);
        public static readonly CustomerEnqueued Customer3Enqueued = new CustomerEnqueued(SingleCustomerQueueId, Customer3TicketId);
        
        public static CustomerAssignedToCounter Customer1AssignedToCounter(CounterId counterId) => new CustomerAssignedToCounter(SingleCustomerQueueId, Customer1TicketId, counterId);
        public static CustomerAssignedToCounter Customer2AssignedToCounter(CounterId counterId) => new CustomerAssignedToCounter(SingleCustomerQueueId, Customer2TicketId, counterId);
        
        public static CustomerServedByCounter Customer1ServedByCounter(CounterId counterId) => new CustomerServedByCounter(SingleCustomerQueueId, Customer1TicketId, counterId);
        
        public static CustomerRecalledByCounter Customer1RecalledByCounter(CounterId counterId) => new CustomerRecalledByCounter(SingleCustomerQueueId, Customer1TicketId, counterId);
        
        public static WaitingCustomersRemoved WaitingCustomersRemoved(params TicketId[] ticketIds) => new WaitingCustomersRemoved(SingleCustomerQueueId, ticketIds.Select(t => (Guid)t).ToList());
        
        public static OutOfLineCustomerAssignedToCounter OutOfLineCustomer1AssignedToCounter(CounterId counterId) => new OutOfLineCustomerAssignedToCounter(SingleCustomerQueueId, Customer1TicketId, counterId);
        public static OutOfLineCustomerAssignedToCounter OutOfLineCustomer2AssignedToCounter(CounterId counterId) => new OutOfLineCustomerAssignedToCounter(SingleCustomerQueueId, Customer2TicketId, counterId);
    }
}