using System.Linq;
using Common.Messaging;
using Ports.EventStore;
using QuerySide.QuerySidePorts;
using QuerySide.Views.CustomerQueueViews;
using Shared.CustomerQueue;
using Shared.TicketIssuer;
using static System.Console;
using static System.DateTime;

namespace QuerySide.Services
{
    public sealed class QuerySideInitializer
    {
        private readonly IEventStore _eventStore;
        private readonly IDomainEventBus _domainEventBus;
        private readonly IClientNotifier _clientNotifier;
        private readonly CustomerQueueViewHolder _viewHolder;

        public QuerySideInitializer(
            IEventStore eventStore,
            CustomerQueueViewHolder viewHolder, 
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
            WriteLine($"Reconstructing views from event store ...\t\t\t{Now}");
            var totalEventsApplied = _eventStore.LoadAllFor<CustomerQueueSubscription>().Select(e => _viewHolder.Apply(e)).Count();
            totalEventsApplied += _eventStore.LoadAllFor<TicketIssuerSubscription>().Select(e => _viewHolder.Apply(e)).Count();
            WriteLine($"All views reconstructed. (Total events applied: {totalEventsApplied})\t\t{Now}");
        }
    }
}