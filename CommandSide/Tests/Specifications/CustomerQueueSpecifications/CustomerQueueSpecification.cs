using System;
using CommandSidePorts.Repositories;
using Common.Messaging;
using Domain;
using InMemory;
using Shared.CustomerQueue;
using static Domain.AvailableCounters;
using static Domain.QueuedTickets;

namespace Tests.Specifications.CustomerQueueSpecifications
{
    public abstract class CustomerQueueSpecification<T> : Specification<CustomerQueue, CustomerQueueCreated, CustomerQueueEvent, T>
        where T : ICommand
    {
        protected ICustomerQueueRepository CustomerQueueRepository => (ICustomerQueueRepository) AggregateRepository;
        
        protected CustomerQueueSpecification(Guid aggregateRootId) : base(
            new CustomerQueueInMemoryRepository(new DomainEventMessageBusAggregator()), 
            () => new CustomerQueue(aggregateRootId, 0, NoAvailableCounters, EmptyQueuedTickets))
        {
        }
    }
}