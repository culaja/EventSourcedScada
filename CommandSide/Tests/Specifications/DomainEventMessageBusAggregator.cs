using System.Collections.Generic;
using System.Linq;
using Common.Messaging;

namespace CommandSide.Tests.Specifications
{
    public sealed class DomainEventMessageBusAggregator : IDomainEventBus
    {
        private readonly List<IDomainEvent> _producedEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> ProducedEvents => _producedEvents;

        public IReadOnlyList<IMessage> DispatchAll(IReadOnlyList<IMessage> messages) =>
            messages.Select(Dispatch).ToList();

        public IMessage Dispatch(IMessage message)
        {
            if (message is IDomainEvent de)
            {
                _producedEvents.Add(de);
            }

            return message;
        }
    }
}