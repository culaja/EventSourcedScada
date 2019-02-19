using System;
using System.Collections.Generic;

namespace Shared.CustomerQueue
{
    public sealed class CustomerTaken : CustomerQueueEvent
    {
        public Guid CounterId { get; }
        public Guid TickedId { get; }

        public CustomerTaken(
            Guid aggregateRootId,
            Guid counterId,
            Guid tickedId) : base(aggregateRootId)
        {
            CounterId = counterId;
            TickedId = tickedId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return CounterId;
            yield return TickedId;
        }
    }
}