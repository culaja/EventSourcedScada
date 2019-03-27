using System;
using System.Collections.Generic;

namespace Shared.CustomerQueue.Events
{
    public sealed class CounterAdded : CustomerQueueEvent
    {
        public string CounterId { get; }

        public CounterAdded(
            Guid aggregateRootId,
            string counterId) : base(aggregateRootId)
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