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
        public static readonly CounterAdded Counter2Added = new CounterAdded(SingleCustomerQueueId, Counter1Id, Counter1Name);
        public static readonly CounterAdded Counter3Added = new CounterAdded(SingleCustomerQueueId, Counter1Id, Counter1Name);
    }
}