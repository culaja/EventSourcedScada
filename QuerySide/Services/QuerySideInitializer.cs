using System;
using Common.Messaging;
using Ports;

namespace Services
{
    public sealed class QuerySideInitializer
    {
        private readonly IEventStoreReader _eventStoreReader;
        private readonly IRemoteEventSubscriber _remoteEventSubscriber;
        private readonly ILocalMessageBus _localMessageBus;

        public QuerySideInitializer(
            IEventStoreReader eventStoreReader,
            IRemoteEventSubscriber remoteEventSubscriber,
            ILocalMessageBus localMessageBus)
        {
            _eventStoreReader = eventStoreReader;
            _remoteEventSubscriber = remoteEventSubscriber;
            _localMessageBus = localMessageBus;
        }

        public void Initialize()
        {
            SubscribeToDomainEventsAndPassThemToLocalMessageBus();
            PerformIntegrityLoadFromEventStore();
        }

        private void SubscribeToDomainEventsAndPassThemToLocalMessageBus()
        {
        }

        private void PerformIntegrityLoadFromEventStore()
        {
            Console.WriteLine("Performing integrity read of domain events ...");
            var domainEvents = _eventStoreReader.LoadAll();
            foreach (var e in domainEvents)
            {
            }
            Console.WriteLine("Integrity read finished");
        }
    }
}