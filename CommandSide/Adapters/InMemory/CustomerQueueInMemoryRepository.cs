using System;
using System.Collections.Generic;
using Common;
using Common.Messaging;
using Domain;
using Ports.Repositories;
using Shared.CustomerQueue;
using static Common.Result;

namespace InMemory
{
    public sealed class CustomerQueueInMemoryRepository : InMemoryRepository<CustomerQueue, CustomerQueueCreated>, ICustomerQueueRepository
    {
        public CustomerQueueInMemoryRepository(ILocalMessageBus localMessageBus) : base(localMessageBus)
        {
        }

        protected override CustomerQueue CreateInternalFrom(CustomerQueueCreated customerQueueCreated) =>
            new CustomerQueue(
                customerQueueCreated.AggregateRootId,
                customerQueueCreated.Version,
                new List<Counter>());

        public Result<CustomerQueue> BorrowSingle(Func<CustomerQueue, Result<CustomerQueue>> transformer) =>
            MaybeFirst.Unwrap(
                customerQueue => transformer(customerQueue),
                () => Fail<CustomerQueue>($"There is no '{nameof(CustomerQueue)}' stored in the repository."));
    }
}