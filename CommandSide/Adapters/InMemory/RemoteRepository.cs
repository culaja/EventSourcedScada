using System;
using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.RemoteDomain;
using Common;
using Common.Messaging;
using Shared.Remote.Events;
using static System.Guid;
using static CommandSide.Domain.RemoteDomain.Remote;

namespace CommandSide.Adapters.InMemory
{
    public sealed class RemoteRepository : InMemoryRepository<Remote, RemoteCreated>, IRemoteRepository
    {
        public RemoteRepository(IDomainEventBus domainEventBus) : base(domainEventBus)
        {
        }

        protected override Remote CreateInternalFrom(RemoteCreated remoteCreated) =>
            new Remote(remoteCreated.AggregateRootId);
    }
}