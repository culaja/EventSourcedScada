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
        /// Can be called only once to perform integrity load of all events in the event store.
        /// </summary>
        /// <returns>Returns all domain events from the event store. <see cref="IEnumerable{T}"/> is used to avoid high memory consumption.</returns>
        IEnumerable<IDomainEvent> IntegrityLoadEvents();

        /// <summary>
        /// Registered callback will be called only after integrity load is performed. In the mean
        /// time all received events will be aggregated.
        /// </summary>
        Nothing Register(EventStoreSubscriptionHandler callback);
    }
}