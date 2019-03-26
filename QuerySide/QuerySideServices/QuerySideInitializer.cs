using System.Linq;
using Common.Messaging;
using Ports.EventStore;
using QuerySide.QuerySidePorts;
using QuerySide.Views;
using static System.Console;
using static System.DateTime;

namespace QuerySide.Services
{
    public sealed class QuerySideInitializer
    {
        private readonly IEventStore _eventStore;
        private readonly IDomainEventBus _domainEventBus;
        private readonly IClientNotifier _clientNotifier;
        private readonly ViewsHolder _viewHolder;

        public QuerySideInitializer(
            IEventStore eventStore,
            ViewsHolder viewHolder,
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
            var totalEventsApplied = _eventStore.LoadAll().Select(e => _viewHolder.Apply(e)).Count();
            WriteLine($"All views reconstructed. (Total events applied: {totalEventsApplied})\t\t{Now}");
        }
    }
}