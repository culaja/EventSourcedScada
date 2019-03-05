using System;
using CommandSidePorts.Repositories;
using Common;
using Common.Messaging;
using Domain;
using Shared.CustomerQueue;
using static System.Guid;
using static Domain.AvailableCounters;
using static Domain.CustomerQueue;
using static Domain.QueuedTickets;

namespace InMemory
{
    public sealed class CustomerQueueInMemoryRepository : InMemoryRepository<CustomerQueue, CustomerQueueCreated>, ICustomerQueueRepository
    {
        public CustomerQueueInMemoryRepository(IDomainEventBus domainEventBus) : base(domainEventBus)
        {
        }

        protected override CustomerQueue CreateInternalFrom(CustomerQueueCreated customerQueueCreated) =>
            new CustomerQueue(
                customerQueueCreated.AggregateRootId,
                customerQueueCreated.Version,
                NoAvailableCounters,
                EmptyQueuedTickets);

        public Result<CustomerQueue> BorrowSingle(Func<CustomerQueue, Result<CustomerQueue>> transformer) =>
            ExecuteTransformerAndPurgeEvents(
                MaybeFirst.Unwrap(
                    customerQueue => customerQueue,
                    () =>  AddNew(NewCustomerQueueFrom(NewGuid())).Value),
                transformer);
    }
}