using System;
using Common.Messaging;

namespace Shared.Remote.Events
{
    public sealed class RemoteCreated : RemoteEvent, IAggregateRootCreated
    {
        public RemoteCreated(Guid aggregateRootId) : base(aggregateRootId)
        {
        }
    }
}