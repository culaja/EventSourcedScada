using System;
using System.Collections.Generic;
using Common;
using Common.Messaging;
using EventStore.MongoDbProvider;
using EventStore.RabbitMqProvider;
using Ports;
using RabbitMQ.Client;
using static Common.Nothing;

namespace EventStore
{
    public sealed class EventStoreSubscription<T> : IEventStoreSubscription<T> where T : IAggregateEventSubscription, new()
    {
        private readonly MongoDbReader<T> _mongoDbReader;
        private readonly RabbitMqSubscriber _rabbitMqSubscriber;
        private readonly DomainEventAggregator _domainEventAggregator = new DomainEventAggregator();

        private Maybe<EventStoreSubscriptionHandler> _maybeEventStoreSubscriptionHandler;
        private bool _isIntegrityLoadPerformed;

        public EventStoreSubscription(
            DatabaseContext databaseContext,
            IConnection rabbitMqConnection)
        {
            _mongoDbReader = new MongoDbReader<T>(databaseContext);
            _rabbitMqSubscriber = new RabbitMqSubscriber(rabbitMqConnection);
        }

        public IEnumerable<IDomainEvent> IntegrityLoadEvents() => _isIntegrityLoadPerformed
            .OnBoth(
                ThrowInvalidOperationException,
                PerformIntegrityLoad);

        private static IEnumerable<IDomainEvent> ThrowInvalidOperationException()
        {
            throw new InvalidOperationException($"Integrity load is already performed for aggregate topic '{nameof(T)}'");
        }

        private IEnumerable<IDomainEvent> PerformIntegrityLoad() => _maybeEventStoreSubscriptionHandler
            .Unwrap(
                PerformIntegrityLoadInternal,
                () => throw new InvalidOperationException($"Integrity load can't be performed if '{nameof(EventStoreSubscriptionHandler)} is not registered.'"));

        private IEnumerable<IDomainEvent> PerformIntegrityLoadInternal(EventStoreSubscriptionHandler callback)
        {
            _isIntegrityLoadPerformed = true;
            foreach (var e in _mongoDbReader.LoadAll()) yield return e;
            _domainEventAggregator.StopAggregation(callback);
        }

        public Nothing Register(EventStoreSubscriptionHandler callback)
        {
            _maybeEventStoreSubscriptionHandler = callback;
            return NotAtAll;
        }

        public void Dispose()
        {
            _rabbitMqSubscriber.Dispose();
            _domainEventAggregator.Dispose();
        }
    }
}