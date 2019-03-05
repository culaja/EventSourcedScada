using System;
using CommandSide.Adapters.InMemory;
using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain;
using Common.Messaging;
using Shared.CustomerQueue;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications
{
    public abstract class CustomerQueueSpecification<T> : Specification<CustomerQueue, CustomerQueueCreated, CustomerQueueEvent, T>
        where T : ICommand
    {
        protected ICustomerQueueRepository CustomerQueueRepository => (ICustomerQueueRepository) AggregateRepository;
        
        protected CustomerQueueSpecification(Guid aggregateRootId) : base(
            new CustomerQueueInMemoryRepository(new DomainEventMessageBusAggregator()), 
            () => new CustomerQueue(aggregateRootId, 0, AvailableCounters.NoAvailableCounters, QueuedTickets.EmptyQueuedTickets))
        {
        }
    }
}