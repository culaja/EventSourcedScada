using System;
using System.Collections.Generic;
using Common;
using Common.Messaging;

namespace Ports
{
    /// <summary>
    /// Describes all functionality which one query side application needs to use to access all events from event store.
    /// User of this interface can be sure that all events are delivered in correct order and that no event is skipped.
    /// </summary>
    /// <typeparam name="T">Represents subscription to a specified aggregate stream.</typeparam>
    public interface IEventStoreSubscription<T>: IDisposable where T : IAggregateEventSubscription
    {
        /// <summary>
        /// Can be called only once to perform integrity load of all events in the event store. After integrity load is
        /// finished all other events will be passed through specified callback.
        /// </summary>
        /// <param name="eventStoreSubscriptionCallback">Callback that will be called on each received event after integrity load is finished.</param>
        /// <returns>
        /// Returns all domain events from the event store. To take advantage of <see cref="IEnumerable{T}"/>  don't
        /// call ToList() or something similar to avoid high memory consumption.
        /// </returns>
        IEnumerable<IDomainEvent> IntegrityLoadEvents(EventStoreSubscriptionHandler eventStoreSubscriptionCallback);
    }
}