using System;
using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain;
using Ports.EventStore;
using Shared.CustomerQueue;

namespace CommandSide.DomainServices
{
    public sealed class AggregateConstructor
    {
        private readonly IEventStore _eventStore;
        private readonly ICustomerQueueRepository _customerQueueRepository;

        public AggregateConstructor(
            IEventStore eventStore,
            ICustomerQueueRepository customerQueueRepository)
        {
            _eventStore = eventStore;
            _customerQueueRepository = customerQueueRepository;
        }

        public void ReconstructAllAggregates()
        {
            Console.WriteLine("Started applying events " + DateTime.Now);
            var totalEventsApplied = _eventStore.ApplyAllTo<CustomerQueue, CustomerQueueCreated, CustomerQueueSubscription>(_customerQueueRepository);
            Console.WriteLine($"Total events applied for '{nameof(CustomerQueue)}': {totalEventsApplied}");
            Console.WriteLine("Finished applying events " + DateTime.Now);
        }
    }
}