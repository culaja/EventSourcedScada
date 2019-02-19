using System;

namespace Shared.CustomerQueue
{
    public sealed class CounterAdded : CustomerQueueEvent
    {
        public Guid CounterId { get; }
        public string CounterName { get; }

        public CounterAdded(
            Guid aggregateRootId,
            Guid counterId,
            string counterName) : base(aggregateRootId)
        {
            CounterId = counterId;
            CounterName = counterName;
        }
    }
}