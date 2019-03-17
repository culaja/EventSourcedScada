using System.Collections.Generic;
using Common.Messaging;

namespace Ports.EventStore
{
    public interface IEventStore
    {
        IDomainEvent Append(IDomainEvent domainEvent);

        IEnumerable<IDomainEvent> LoadAll();

        IEnumerable<IDomainEvent> LoadAllFor<T>() where T: IAggregateEventSubscription, new();
    }
}