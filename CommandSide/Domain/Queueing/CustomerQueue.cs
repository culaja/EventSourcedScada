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
            foreach (var counterDetail in c.CountersDetails)
            {
                ApplyChange(new CounterAdded(Id, counterDetail.Id, counterDetail.Name));  
            }
            
            foreach (var openTime in c.OpenTimes)
            {
                ApplyChange(new OpenTimeAdded(Id, openTime.Day, openTime.BeginTimestamp, openTime.EndTimestamp));  
            }
            
            return Ok(this);
        }
        
        private CustomerQueue Apply(CounterAdded e) => this;
        
        private CustomerQueue Apply(OpenTimeAdded e) => this;
    }
}