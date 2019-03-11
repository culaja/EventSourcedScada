using System;
using System.Linq;
using Common;
using Common.Messaging;
using static Common.Nothing;

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
                    repository.CreateFrom(aggregateRootCreated)
                        .OnSuccess(aggregateRoot => TryToApplyToAggregate(aggregateRoot, aggregateRootCreated));
                    break;
                default:
                    repository.BorrowBy(
                        domainEvent.AggregateRootId,
                        t => TryToApplyToAggregate(t, domainEvent));
                    break;
            }

            return domainEvent;
        }

        private static Result<T> TryToApplyToAggregate<T>(T aggregateRoot, IDomainEvent e) where T : AggregateRoot
        {
            aggregateRoot.CheckAggregateRootVersionAgainst(e);
            aggregateRoot.ApplyFrom(e);
            return aggregateRoot.ToOkResult();
        }
        
        private static Nothing CheckAggregateRootVersionAgainst<T>(this T aggregateRoot, IDomainEvent e) where T : AggregateRoot
        {
            var expectedVersion = aggregateRoot.Version + 1;
            if (expectedVersion != e.Version)
            {
                throw new InvalidOperationException($"Expected to apply {expectedVersion} event version of Aggregate '{typeof(T).Name}' with ID '{aggregateRoot.Id}', but version {e.Version} received. (Event: {e})");
            }

            return NotAtAll;
        }
    }
}