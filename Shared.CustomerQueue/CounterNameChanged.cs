using System;
using System.Collections.Generic;

namespace Shared.CustomerQueue
{
    public sealed class CounterNameChanged : CustomerQueueEvent
    {
        public Guid CounterId { get; }
        public string NewCounterName { get; }

        public CounterNameChanged(
            Guid aggregateRootId,
            Guid counterId,
            string newCounterName) : base(aggregateRootId)
        {
            CounterId = counterId;
            NewCounterName = newCounterName;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return CounterId;
            yield return NewCounterName;
        }
    }
}