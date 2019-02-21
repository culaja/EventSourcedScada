using System.Linq;
using Common;
using Common.Messaging;

namespace Ports.EventStore
{
    public static class EventApplier
    {
        public static int ApplyAllTo<T, TK, TL>(this IEventStore eventStore, IRepository<T, TK> repository) 
            where T : AggregateRoot 
            where TK : IAggregateRootCreated 
            where TL : IAggregateEventSubscription, new() => eventStore
            .LoadAllFor<TL>()
            .Select(domainEvent => HandleBasedOnType(domainEvent, repository))
            .Count();

        private static IDomainEvent HandleBasedOnType<T, Tk>(
            IDomainEvent domainEvent,
            IRepository<T, Tk> repository) 
            where T : AggregateRoot
            where Tk: IAggregateRootCreated
        {
            switch (domainEvent)
            {
                case Tk aggregateRootCreated:
                    repository.CreateFrom(aggregateRootCreated);
                    break;
                default:
                    repository.BorrowBy(
                        domainEvent.AggregateRootId,
                        t => ApplyToAggregate(t, domainEvent));
                    break;
            }

            return domainEvent;
        }

        private static Result<T> ApplyToAggregate<T>(T aggregateRoot, IDomainEvent e) where T : AggregateRoot
        {
            aggregateRoot.ApplyFrom(e);
            return aggregateRoot.ToOkResult();
        }
    }
}