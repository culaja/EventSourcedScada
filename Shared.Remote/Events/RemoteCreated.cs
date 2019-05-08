using System;
using Common.Messaging;

namespace Shared.Remote.Events
{
    public sealed class RemoteCreated : RemoteEvent, IAggregateRootCreated
    {
        public string RemoteName { get; }

        public RemoteCreated(
            Guid aggregateRootId,
            string remoteName) : base(aggregateRootId)
        {
            RemoteName = remoteName;
        }
    }
}