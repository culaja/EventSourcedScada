using System;
using System.Collections.Generic;
using Common.Messaging;
using Domain;
using InMemory;
using Ports.Repositories;
using Shared.CustomerQueue;
using Tests.IntegrationTests;

namespace Tests.Specifications.CustomerQueueSpecifications
{
    public abstract class CustomerQueueSpecification<T> : Specification<CustomerQueue, CustomerQueueCreated, CustomerQueueEvent, T>
        where T : ICommand
    {
        protected ICustomerQueueRepository CustomerQueueRepository => (ICustomerQueueRepository) AggregateRepository;
        
        protected CustomerQueueSpecification() : base(
            new CustomerQueueInMemoryRepository(new NoOpLocalMessageBus()), 
            () => new CustomerQueue(Guid.NewGuid(), 0, new List<Counter>()))
        {
        }
    }
}