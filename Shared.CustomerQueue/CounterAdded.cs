using System;
using System.Collections.Generic;

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

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return CounterId;
            yield return CounterName;
        }
    }
}