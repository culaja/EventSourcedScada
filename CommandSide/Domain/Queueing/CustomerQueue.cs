using System;
using System.Linq;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.Domain.Queueing.Configuring;
using Common;
using Shared.CustomerQueue;
using static Common.Result;

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

        public Result<CustomerQueue> SetConfiguration(Configuration c)
        {
            var firstCounterDetails = c.CountersDetails.Items.First();
            ApplyChange(new CounterAdded(Id, firstCounterDetails.Id, firstCounterDetails.Name));
            return Ok(this);
        }
        
        private CustomerQueue Apply(CounterAdded e) => this;
    }
}