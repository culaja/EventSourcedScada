using System;
using CommandSide.Adapters.InMemory;
using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.RemoteDomain;
using Common.Messaging;
using Shared.Remote;
using Shared.Remote.Events;

namespace CommandSide.Tests.Specifications.RemoteSpecifications
{
    public abstract class RemoteSpecification<T> : Specification<Remote, RemoteCreated, RemoteEvent, T>
        where T : ICommand
    {
        protected IRemoteRepository RemoteRepository => (IRemoteRepository) AggregateRepository;

        protected RemoteSpecification() : base(
            new RemoteRepository(new DomainEventMessageBusAggregator()))
        {
        }
    }
}