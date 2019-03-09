using System;
using System.Collections.Generic;
using Shared.CustomerQueue;
using static CommandSide.Tests.Specifications.CustomerQueueConfigurationTestValues;

namespace CommandSide.Tests.Specifications
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
        
        public static readonly OpenTimeAdded Monday9To12Added = new OpenTimeAdded(SingleCustomerQueueId, Monday9To12.Day, Monday9To12.BeginTimestamp, Monday9To12.EndTimestamp);
        public static readonly OpenTimeAdded Monday14To16Added = new OpenTimeAdded(SingleCustomerQueueId, Monday14To16.Day, Monday14To16.BeginTimestamp, Monday14To16.EndTimestamp);
        public static readonly OpenTimeAdded Tuesday9To12Added = new OpenTimeAdded(SingleCustomerQueueId, Tuesday9To12.Day, Tuesday9To12.BeginTimestamp, Tuesday9To12.EndTimestamp);
        public static readonly IReadOnlyList<OpenTimeAdded> AllOpenTimesAdded = new[] {Monday9To12Added, Monday14To16Added, Tuesday9To12Added};
        public static readonly IReadOnlyList<OpenTimeAdded> MondayOpenTimesAdded = new[] {Monday9To12Added, Monday14To16Added};
        
        public static readonly CounterRemoved Counter1Removed = new CounterRemoved(SingleCustomerQueueId, Counter1Id);
        public static readonly CounterRemoved Counter2Removed = new CounterRemoved(SingleCustomerQueueId, Counter2Id);
        public static readonly CounterRemoved Counter3Removed = new CounterRemoved(SingleCustomerQueueId, Counter3Id);
        public static readonly IReadOnlyList<CounterRemoved> AllCountersRemoved = new[] {Counter1Removed, Counter2Removed, Counter3Removed};
        
        public static readonly OpenTimeRemoved Monday9To12Removed = new OpenTimeRemoved(SingleCustomerQueueId, Monday9To12.Day, Monday9To12.BeginTimestamp, Monday9To12.EndTimestamp);
        public static readonly OpenTimeRemoved Monday14To16Removed = new OpenTimeRemoved(SingleCustomerQueueId, Monday14To16.Day, Monday14To16.BeginTimestamp, Monday14To16.EndTimestamp);
        public static readonly OpenTimeRemoved Tuesday9To12Removed = new OpenTimeRemoved(SingleCustomerQueueId, Tuesday9To12.Day, Tuesday9To12.BeginTimestamp, Tuesday9To12.EndTimestamp);
        public static readonly IReadOnlyList<OpenTimeRemoved> AllOpenTimesRemoved = new[] {Monday9To12Removed, Monday14To16Removed, Tuesday9To12Removed};
        
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
    }
}