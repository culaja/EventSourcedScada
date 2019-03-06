using System;
using Shared.CustomerQueue;

namespace CommandSide.Tests.Specifications
{
    public static class CustomerQueueTestValues
    {
        public static readonly Guid SingleCustomerQueueId = Guid.NewGuid();
        
        public static readonly CustomerQueueCreated SingleCustomerQueueCreated = new CustomerQueueCreated(SingleCustomerQueueId); 
    }
}