using System;
using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.RemoteDomain;
using Common;
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

        protected override void AddedNew(Remote aggregateRoot)
        {
            base.AddedNew(aggregateRoot);
            AddNewIndex(aggregateRoot.RemoteName, aggregateRoot);
        }

        protected override Result ContainsKey(Remote aggregateRoot) => 
            base.ContainsKey(aggregateRoot)
                .OnSuccess(() => ContainsIndex(aggregateRoot.RemoteName));

        public Result BorrowBy(RemoteName remoteName, Func<Remote, Result<Remote>> transformer) =>
            MaybeReadIndex(remoteName)
                .OnSuccess(remote => ExecuteTransformerAndPurgeEvents(remote, transformer));
    }
}