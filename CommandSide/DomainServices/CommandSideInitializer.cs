using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.Queueing;
using Common;
using Ports.EventStore;
using Shared.CustomerQueue;
using static System.Console;
using static System.DateTime;
using static Common.Nothing;

namespace CommandSide.DomainServices
{
    public sealed class CommandSideInitializer
    {
        private readonly IEventStore _eventStore;
        private readonly ICustomerQueueRepository _customerQueueRepository;

        public CommandSideInitializer(
            IEventStore eventStore,
            ICustomerQueueRepository customerQueueRepository)
        {
            _eventStore = eventStore;
            _customerQueueRepository = customerQueueRepository;
        }

        public Nothing Initialize() => ReconstructAllAggregates();

        private Nothing ReconstructAllAggregates()
        {
            WriteLine($"Reconstructing aggregates from event store ...\t\t\t{Now}");
            var totalEventsApplied = _eventStore.ApplyAllTo<CustomerQueue, CustomerQueueCreated, CustomerQueueSubscription>(_customerQueueRepository);
            WriteLine($"All aggregates reconstructed. (Total applied events: {totalEventsApplied})\t{Now}");
            
            return NotAtAll;
        }
    }
}