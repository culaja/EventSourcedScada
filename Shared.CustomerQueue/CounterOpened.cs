using System;
using System.Collections.Generic;

namespace Shared.CustomerQueue
{
    public sealed class CounterOpened : CustomerQueueEvent
    {
        public int CounterId { get; }

        public CounterOpened(
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