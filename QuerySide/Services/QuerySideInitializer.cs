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
        private readonly ILocalMessageBus _localMessageBus;
        private readonly IClientNotifier _clientNotifier;
        private readonly ViewHolder _viewHolder;

        public QuerySideInitializer(
            IEventStoreSubscription<CustomerQueueSubscription> customerQueueEventStoreSubscription,
            ILocalMessageBus localMessageBus,
            IClientNotifier clientNotifier,
            ViewHolder viewHolder)
        {
            _customerQueueEventStoreSubscription = customerQueueEventStoreSubscription;
            _localMessageBus = localMessageBus;
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
            _clientNotifier.StartClientNotifier(() => _localMessageBus.Dispatch(new NewClientConnected()));
        }

        private void FeedFromEventStore()
        {
            _customerQueueEventStoreSubscription.Register(e =>
            {
                _localMessageBus.Dispatch(e);
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