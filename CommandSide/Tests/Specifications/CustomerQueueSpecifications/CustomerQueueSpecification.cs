using System;
using System.Collections.Generic;
using Common.Messaging;
using Domain;
using InMemory;
using Ports.Repositories;
using Shared.CustomerQueue;
using Tests.IntegrationTests;
using static Domain.AvailableCounters;
using static Domain.QueuedTickets;

namespace Tests.Specifications.CustomerQueueSpecifications
{
    public abstract class CustomerQueueSpecification<T> : Specification<CustomerQueue, CustomerQueueCreated, CustomerQueueEvent, T>
        where T : ICommand
    {
        protected ICustomerQueueRepository CustomerQueueRepository => (ICustomerQueueRepository) AggregateRepository;
        
        protected CustomerQueueSpecification(Guid aggregateRootId) : base(
            new CustomerQueueInMemoryRepository(new NoOpLocalMessageBus()), 
            () => new CustomerQueue(aggregateRootId, 0, NoAvailableCounters, EmptyQueuedTickets))
        {
        }
    }
}