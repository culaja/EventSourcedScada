using System;
using Common;
using Shared.CustomerQueue.Events;

namespace CommandSide.Domain.Queueing
{
    public sealed class CustomerQueue : AggregateRoot
    {
        public CustomerQueue(Guid id) : base(id)
        {
        }

        public static CustomerQueue NewCustomerQueueFrom(
            Guid id)
        {
            var customerQueue = new CustomerQueue(id);
            customerQueue.ApplyChange(new CustomerQueueCreated(customerQueue.Id));
            return customerQueue;
        }

        private CustomerQueue Apply(CustomerQueueCreated _) => this;
    }
}