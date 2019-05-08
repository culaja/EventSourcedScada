using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.RemoteDomain;
using Common.Messaging;
using Shared.Remote.Events;
using static CommandSide.Domain.RemoteDomain.RemoteName;

namespace CommandSide.Adapters.InMemory
{
    public sealed class RemoteRepository : InMemoryRepository<Remote, RemoteCreated>, IRemoteRepository
    {
        public RemoteRepository(IDomainEventBus domainEventBus) : base(domainEventBus)
        {
        }

        protected override Remote CreateInternalFrom(RemoteCreated remoteCreated) =>
            new Remote(
                remoteCreated.AggregateRootId,
                RemoteNameFrom(remoteCreated.RemoteName));
    }
}