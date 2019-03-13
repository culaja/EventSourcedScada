using Common;
using Common.Messaging;
using Ports.EventStore;
using Shared.TicketIssuer;

namespace CommandSide.DomainServices.TicketIssuing.EventHandlers
{
    public sealed class TickerIssuerPersistenceHandler : EventHandler<TicketIssuerEvent>
    {
        private readonly IEventStore _eventStore;

        public TickerIssuerPersistenceHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public override Result Handle(TicketIssuerEvent e) => _eventStore.Append(e)
            .ToOkResult();
    }
}