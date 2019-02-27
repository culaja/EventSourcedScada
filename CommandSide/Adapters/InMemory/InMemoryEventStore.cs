using System.Collections.Generic;
using System.Linq;
using Common.Messaging;
using Ports.EventStore;

namespace InMemory
{
    public sealed class InMemoryEventStore : IEventStore
    {
        private readonly List<IDomainEvent> _allDomainEvents = new List<IDomainEvent>();
        
        public IDomainEvent Append(IDomainEvent domainEvent)
        {
            _allDomainEvents.Add(domainEvent);
            return domainEvent;
        }

        public IEnumerable<IDomainEvent> LoadAllFor<T>() where T : IAggregateEventSubscription, new()
        {
            var aggregateEventSubscription = new T();
            return _allDomainEvents
                .Where(domainEvent => domainEvent.AggregateTopicName == aggregateEventSubscription.AggregateTopicName).ToList();
        }
    }
}