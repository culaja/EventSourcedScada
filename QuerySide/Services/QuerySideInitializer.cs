using System;
using Common;
using Common.Messaging;
using CustomerQueueViews;
using Ports.EventStore;
using QuerySidePorts;
using Shared.CustomerQueue;
using static Common.Nothing;

namespace Services
{
    public sealed class QuerySideInitializer
    {
        private readonly IEventStore _eventStore;
        private readonly IDomainEventBus _domainEventBus;
        private readonly IClientNotifier _clientNotifier;
        private readonly ViewHolder _viewHolder;

        public QuerySideInitializer(
            IEventStore eventStore,
            ViewHolder viewHolder, 
            IDomainEventBus domainEventBus,
            IClientNotifier clientNotifier)
        {
            _eventStore = eventStore;
            _viewHolder = viewHolder;
            _domainEventBus = domainEventBus;
            _clientNotifier = clientNotifier;
        }

        public void Initialize()
        {
            StartClientNotifier();
            IntegrityLoadEventsFromEventStore();
        }

        private void StartClientNotifier()
        {
            _clientNotifier.StartClientNotifier(() => _domainEventBus.Dispatch(new NewClientConnected()));
        }

        private void IntegrityLoadEventsFromEventStore()
        {
            Console.WriteLine("Performing integrity read of domain events ...");
            foreach (var e in _eventStore.LoadAllFor<CustomerQueueSubscription>()) _viewHolder.Apply(e);
            Console.WriteLine("Integrity read finished");
            Console.WriteLine(_viewHolder);
        }
    }
}