using System;

namespace Shared.CustomerQueue
{
    public sealed class CustomerEnqueued : CustomerQueueEvent
    {
        public CustomerEnqueued(Guid aggregateRootId) : base(aggregateRootId)
        {
        }
    }
}