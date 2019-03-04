using System;
using Common.Messaging;
using CustomerQueueViews;
using Ports;
using Shared.CustomerQueue;
using static Common.Nothing;

namespace Services
{
    public sealed class QuerySideInitializer
    {
        private readonly IEventStoreSubscription<CustomerQueueSubscription> _customerQueueEventStoreSubscription;
        private readonly IDomainEventBus _domainEventBus;
        private readonly IClientNotifier _clientNotifier;
        private readonly ViewHolder _viewHolder;

        public QuerySideInitializer(
            IEventStoreSubscription<CustomerQueueSubscription> customerQueueEventStoreSubscription,
            IDomainEventBus domainEventBus,
            IClientNotifier clientNotifier,
            ViewHolder viewHolder)
        {
            _customerQueueEventStoreSubscription = customerQueueEventStoreSubscription;
            _domainEventBus = domainEventBus;
            _clientNotifier = clientNotifier;
            _viewHolder = viewHolder;
        }

        public void Initialize()
        {
            StartClientNotifier();
            FeedFromEventStore();
        }

        private void StartClientNotifier()
        {
            _clientNotifier.StartClientNotifier(() => _domainEventBus.Dispatch(new NewClientConnected()));
        }

        private void FeedFromEventStore()
        {
            _customerQueueEventStoreSubscription.Register(e =>
            {
                _domainEventBus.Dispatch(e);
                return NotAtAll;
            });
            
            Console.WriteLine("Performing integrity read of domain events ...");
            var domainEvents = _customerQueueEventStoreSubscription.IntegrityLoadEvents();
            foreach (var e in domainEvents) _viewHolder.Apply(e);
            Console.WriteLine("Integrity read finished");
            Console.WriteLine(_viewHolder);
        }
    }
}