using System;
using System.Collections.Generic;
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
        public static readonly CounterOpened Counter2Opened = new CounterOpened(SingleCustomerQueueId, Counter2Id);
        public static readonly CounterOpened Counter3Opened = new CounterOpened(SingleCustomerQueueId, Counter3Id);
        
        public static readonly CounterClosed Counter1Closed = new CounterClosed(SingleCustomerQueueId, Counter1Id);
        public static readonly CounterClosed Counter2Closed = new CounterClosed(SingleCustomerQueueId, Counter2Id);
        public static readonly CounterClosed Counter3Closed = new CounterClosed(SingleCustomerQueueId, Counter3Id);
        
        public static readonly CustomerEnqueued Customer1Enqueued = new CustomerEnqueued(SingleCustomerQueueId);
    }
}