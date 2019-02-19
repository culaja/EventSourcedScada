using System;
using Common.Messaging;

namespace Shared.CustomerQueue
{
    public sealed class CustomerQueueCreated : CustomerQueueEvent, IAggregateRootCreated
    {
        public CustomerQueueCreated(Guid aggregateRootId) : base(aggregateRootId)
        {
        }
    }
}