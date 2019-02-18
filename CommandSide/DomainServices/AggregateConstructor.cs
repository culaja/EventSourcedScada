using System;
using Ports.EventStore;

namespace DomainServices
{
    public sealed class AggregateConstructor
    {
        private readonly IEventStore _eventStore;

        public AggregateConstructor(
            IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public void ReconstructAllAggregates()
        {
            Console.WriteLine("Started applying events " + DateTime.Now);
            Console.WriteLine("Finished applying events " + DateTime.Now);
        }
    }
}