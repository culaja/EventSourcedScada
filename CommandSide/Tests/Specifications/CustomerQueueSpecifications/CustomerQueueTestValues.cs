using System;
using Shared.CustomerQueue.Events;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications
{
    public static class CustomerQueueTestValues
    {
        public static readonly Guid SingleCustomerQueueId = Guid.NewGuid();

        public static readonly CustomerQueueCreated SingleCustomerQueueCreated = new CustomerQueueCreated(SingleCustomerQueueId);
    }
}