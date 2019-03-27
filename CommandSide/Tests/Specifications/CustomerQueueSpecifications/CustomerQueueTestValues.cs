using System;
using CommandSide.Domain.Queueing;
using Shared.CustomerQueue.Events;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications
{
    public static class CustomerQueueTestValues
    {
        public static readonly Guid SingleCustomerQueueId = Guid.NewGuid();

        public static readonly CustomerQueueCreated SingleCustomerQueueCreated = new CustomerQueueCreated(SingleCustomerQueueId);
        
        public static readonly CounterId Counter1Id = new CounterId("Counter1");
        
        public static readonly CounterAdded Counter1Added = new CounterAdded(SingleCustomerQueueId, Counter1Id);
    }
}