using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.Queueing;
using CommandSide.Domain.TicketIssuing;
using Common;
using Ports.EventStore;
using Shared.CustomerQueue;
using Shared.TicketIssuer;
using static System.Console;
using static System.DateTime;
using static Common.Nothing;

namespace CommandSide.DomainServices
{
    public sealed class CommandSideInitializer
    {
        private readonly IEventStore _eventStore;
        private readonly ICustomerQueueRepository _customerQueueRepository;
        private readonly ITicketIssuerRepository _ticketIssuerRepository;

        public CommandSideInitializer(
            IEventStore eventStore,
            ICustomerQueueRepository customerQueueRepository,
            ITicketIssuerRepository ticketIssuerRepository)
        {
            _eventStore = eventStore;
            _customerQueueRepository = customerQueueRepository;
            _ticketIssuerRepository = ticketIssuerRepository;
        }

        public Nothing Initialize() => ReconstructAllAggregates();

        private Nothing ReconstructAllAggregates()
        {
            WriteLine($"Reconstructing aggregates from event store ...\t\t\t{Now}");
            
            var totalEventsAppliedForCustomerQueue = _eventStore.ApplyAllTo<CustomerQueue, CustomerQueueCreated, CustomerQueueSubscription>(_customerQueueRepository);
            WriteLine($"Aggregate {nameof(CustomerQueue)} reconstructed. (Total applied events: {totalEventsAppliedForCustomerQueue})\t{Now}");
            
            var totalEventsAppliedForTicketIssuer = _eventStore.ApplyAllTo<TicketIssuer, TicketIssuerCreated, TicketIssuerSubscription>(_ticketIssuerRepository);
            WriteLine($"Aggregate {nameof(TicketIssuer)} reconstructed. (Total applied events: {totalEventsAppliedForTicketIssuer})\t{Now}");
            
            return NotAtAll;
        }
    }
}