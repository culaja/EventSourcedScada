using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.RemoteDomain;
using Common;
using Ports.EventStore;
using Shared.Remote;
using static System.Console;
using static System.DateTime;
using static Common.Nothing;

namespace CommandSide.DomainServices
{
    public sealed class CommandSideInitializer
    {
        private readonly IEventStore _eventStore;
        private readonly IRemoteRepository _remoteRepository;

        public CommandSideInitializer(
            IEventStore eventStore,
            IRemoteRepository remoteRepository)
        {
            _eventStore = eventStore;
            _remoteRepository = remoteRepository;
        }

        public Nothing Initialize() => ReconstructAllAggregates();

        private Nothing ReconstructAllAggregates()
        {
            WriteLine($"Reconstructing aggregates from event store ...\t\t\t{Now}");

            var totalEventsAppliedForRemote = _eventStore.LoadAllFor<RemoteSubscription>().ApplyAllTo(_remoteRepository);
            
            WriteLine($"Aggregate {nameof(Remote)} reconstructed. (Total applied events: {totalEventsAppliedForRemote})\t{Now}");

            return NotAtAll;
        }
    }
}