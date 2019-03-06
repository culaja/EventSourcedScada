using System;
using Common;
using Shared.CustomerQueue;

namespace CommandSide.Domain.Queueing
{
    public sealed class CustomerQueue : AggregateRoot
    {
        public CustomerQueue(
            Guid id,
            ulong version) : base(id, version)
        {
        }
        
        public static CustomerQueue NewCustomerQueueFrom(
            Guid id)
        {
            var customerQueue = new CustomerQueue(
                id,
                0);
            customerQueue.ApplyChange(new CustomerQueueCreated(
                customerQueue.Id));
            return customerQueue;
        }

        private CustomerQueue Apply(CustomerQueueCreated _) => this;
    }
}