using System.Linq;
using Common;

namespace Ports.EventStore
{
    public static class EventApplier
    {
        public static int ApplyAllTo<T>(this IEventStore eventSource, IRepository<T> eventDestination) where T : AggregateRoot => eventSource
            .LoadAllForAggregateStartingFrom<T>(0)
            .Select(domainEvent => eventDestination
                .Borrow(domainEvent.AggregateRootId, domainEvent.ApplyTo))
            .Count();
    }
}