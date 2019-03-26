using System;
using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.Queueing;
using Common;
using Common.Messaging;
using Shared.CustomerQueue.Events;
using static System.Guid;
using static CommandSide.Domain.Queueing.CustomerQueue;

namespace CommandSide.Adapters.InMemory
{
    public sealed class CustomerQueueInMemoryRepository : InMemoryRepository<CustomerQueue, CustomerQueueCreated>, ICustomerQueueRepository
    {
        public CustomerQueueInMemoryRepository(IDomainEventBus domainEventBus) : base(domainEventBus)
        {
        }

        protected override CustomerQueue CreateInternalFrom(CustomerQueueCreated customerQueueCreated) =>
            new CustomerQueue(customerQueueCreated.AggregateRootId);

        public Result<CustomerQueue> BorrowSingle(Func<CustomerQueue, Result<CustomerQueue>> transformer) =>
            ExecuteTransformerAndPurgeEvents(
                MaybeFirst.Unwrap(
                    customerQueue => customerQueue,
                    () => AddNew(NewCustomerQueueFrom(NewGuid())).Value),
                transformer);
    }
}