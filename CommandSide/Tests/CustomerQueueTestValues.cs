using System;
using CommandSide.Domain;
using Shared.CustomerQueue;

namespace CommandSide.Tests
{
    public static class CustomerQueueTestValues
    {
        public static readonly Guid SingleCustomerQueueId = Guid.NewGuid();
        
        public static readonly CustomerQueueCreated SingleCustomerQueueCreated = new CustomerQueueCreated(SingleCustomerQueueId);
    }
}