using System;
using CommandSide.Adapters.InMemory;
using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.RemoteDomain;
using Common.Messaging;
using Shared.Remote;
using Shared.Remote.Events;

namespace CommandSide.Tests.Specifications.RemoteSpecifications
{
    public abstract class CustomerQueueSpecification<T> : Specification<Remote, RemoteCreated, RemoteEvent, T>
        where T : ICommand
    {
        protected IRemoteRepository CustomerQueueRepository => (IRemoteRepository) AggregateRepository;

        protected CustomerQueueSpecification(Guid aggregateRootId) : base(
            new RemoteRepository(new DomainEventMessageBusAggregator()),
            () => new Remote(aggregateRootId))
        {
        }
    }
}