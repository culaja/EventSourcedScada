using System;
using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain;
using Common;
using Common.Messaging;
using Shared.CustomerQueue;

namespace CommandSide.Adapters.InMemory
{
    public sealed class CustomerQueueInMemoryRepository : InMemoryRepository<CustomerQueue, CustomerQueueCreated>, ICustomerQueueRepository
    {
        public CustomerQueueInMemoryRepository(IDomainEventBus domainEventBus) : base(domainEventBus)
        {
        }

        protected override CustomerQueue CreateInternalFrom(CustomerQueueCreated customerQueueCreated) =>
            new CustomerQueue(
                customerQueueCreated.AggregateRootId,
                customerQueueCreated.Version);

        public Result<CustomerQueue> BorrowSingle(Func<CustomerQueue, Result<CustomerQueue>> transformer) =>
            ExecuteTransformerAndPurgeEvents(
                MaybeFirst.Unwrap(
                    customerQueue => customerQueue,
                    () =>  AddNew(CustomerQueue.NewCustomerQueueFrom(Guid.NewGuid())).Value),
                transformer);
    }
}