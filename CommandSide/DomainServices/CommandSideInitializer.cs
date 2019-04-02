using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.RemoteDomain;
using Common;
using Ports.EventStore;
using Shared.Remote;
using Shared.Remote.Events;
using static System.Console;
using static System.DateTime;
using static Common.Nothing;

namespace CommandSide.DomainServices
{
    public sealed class CommandSideInitializer
    {
        private readonly IEventStore _eventStore;
        private readonly IRemoteRepository _customerQueueRepository;

        public CommandSideInitializer(
            IEventStore eventStore,
            IRemoteRepository customerQueueRepository)
        {
            _eventStore = eventStore;
            _customerQueueRepository = customerQueueRepository;
        }

        public Nothing Initialize() => ReconstructAllAggregates();

        private Nothing ReconstructAllAggregates()
        {
            WriteLine($"Reconstructing aggregates from event store ...\t\t\t{Now}");

            var totalEventsAppliedForCustomerQueue = _eventStore.ApplyAllTo<Remote, RemoteCreated, RemoteSubscription>(_customerQueueRepository);
            WriteLine($"Aggregate {nameof(Remote)} reconstructed. (Total applied events: {totalEventsAppliedForCustomerQueue})\t{Now}");

            return NotAtAll;
        }
    }
}