using System;
using System.Collections.Generic;

namespace Shared.CustomerQueue.Events
{
    public sealed class CounterRemoved : CustomerQueueEvent
    {
        public int CounterId { get; }

        public CounterRemoved(
            Guid aggregateRootId,
            int counterId) : base(aggregateRootId)
        {
            CounterId = counterId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return CounterId;
        }
    }
}