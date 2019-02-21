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
        private readonly CountersView _countersView;
        private readonly TicketsPerCounterServedView _ticketsPerCounterServedView;

        public QuerySideInitializer(
            IEventStoreReader eventStoreReader,
            IRemoteEventSubscriber remoteEventSubscriber,
            ILocalMessageBus localMessageBus,
            CountersView countersView,
            TicketsPerCounterServedView ticketsPerCounterServedView)
        {
            _eventStoreReader = eventStoreReader;
            _remoteEventSubscriber = remoteEventSubscriber;
            _localMessageBus = localMessageBus;
            _countersView = countersView;
            _ticketsPerCounterServedView = ticketsPerCounterServedView;
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
            foreach (var e in domainEvents)
            {
                _countersView.Apply(e);
                _ticketsPerCounterServedView.Apply(e);
            }
            Console.WriteLine("Integrity read finished");
            Console.WriteLine(_countersView);
            Console.WriteLine(_ticketsPerCounterServedView);
        }
    }
}