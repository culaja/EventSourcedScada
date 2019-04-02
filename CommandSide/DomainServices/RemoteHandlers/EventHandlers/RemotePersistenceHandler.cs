using Common;
using Common.Messaging;
using Ports.EventStore;
using Shared.Remote;

namespace CommandSide.DomainServices.RemoteHandlers.EventHandlers
{
    public sealed class RemotePersistenceHandler : EventHandler<RemoteEvent>
    {
        private readonly IEventStore _eventStore;

        public RemotePersistenceHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public override Result Handle(RemoteEvent e) => _eventStore.Append(e)
            .ToOkResult();
    }
}