using System;
using Common.Messaging;
using CustomerQueueViews;
using Ports;
using Shared.CustomerQueue;

namespace Services
{
    public sealed class QuerySideInitializer
    {
        private readonly IEventStoreReader _eventStoreReader;
        private readonly IRemoteEventSubscriber _remoteEventSubscriber;
        private readonly ILocalMessageBus _localMessageBus;
        private readonly ViewHolder _viewHolder;

        public QuerySideInitializer(
            IEventStoreReader eventStoreReader,
            IRemoteEventSubscriber remoteEventSubscriber,
            ILocalMessageBus localMessageBus,
            ViewHolder viewHolder)
        {
            _eventStoreReader = eventStoreReader;
            _remoteEventSubscriber = remoteEventSubscriber;
            _localMessageBus = localMessageBus;
            _viewHolder = viewHolder;
        }

        public void Initialize()
        {
            SubscribeToDomainEventsAndPassThemToLocalMessageBus();
            PerformIntegrityLoadFromEventStore();
        }

        private void SubscribeToDomainEventsAndPassThemToLocalMessageBus()
        {
            _remoteEventSubscriber.Register<CustomerQueueSubscription>(e => _localMessageBus.Dispatch(e));
        }

        private void PerformIntegrityLoadFromEventStore()
        {
            Console.WriteLine("Performing integrity read of domain events ...");
            var domainEvents = _eventStoreReader.LoadAll();
            foreach (var e in domainEvents) _viewHolder.Apply(e);
            Console.WriteLine("Integrity read finished");
            Console.WriteLine(_viewHolder);
        }
    }
}