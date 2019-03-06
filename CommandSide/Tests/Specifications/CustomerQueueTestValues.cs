using System;
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
        
        public static readonly OpenTimeAdded Monday9To12Added = new OpenTimeAdded(SingleCustomerQueueId, Monday9To12.Day, Monday9To12.BeginTimestamp, Monday9To12.EndTimestamp);
        public static readonly OpenTimeAdded Monday14To16Added = new OpenTimeAdded(SingleCustomerQueueId, Monday14To16.Day, Monday14To16.BeginTimestamp, Monday14To16.EndTimestamp);
        public static readonly OpenTimeAdded Tuesday9To12Added = new OpenTimeAdded(SingleCustomerQueueId, Tuesday9To12.Day, Tuesday9To12.BeginTimestamp, Tuesday9To12.EndTimestamp);
    }
}