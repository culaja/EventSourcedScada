using Common;
using Common.Messaging;
using Ports.EventStore;
using Shared.CustomerQueue;

namespace CommandSide.DomainServices.EventHandlers
{
    public sealed class CustomerQueuePersistenceHandler : EventHandler<CustomerQueueEvent>
    {
        private readonly IEventStore _eventStore;

        public CustomerQueuePersistenceHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public override Result Handle(CustomerQueueEvent e) => _eventStore.Append(e)
            .ToOkResult();
    }
}