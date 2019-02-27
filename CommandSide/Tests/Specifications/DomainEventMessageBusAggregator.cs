using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using static Common.Result;

namespace Tests.Specifications
{
    public sealed class DomainEventMessageBusAggregator : ILocalMessageBus
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
        
        public Task<Result> HandleAsync(IMessage message) => Task.FromResult(Ok());
    }
}