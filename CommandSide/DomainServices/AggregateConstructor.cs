using System;
using CommandSidePorts.Repositories;
using Domain;
using Ports.EventStore;
using Shared.CustomerQueue;
using static System.Console;

namespace DomainServices
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
            WriteLine("Started applying events " + DateTime.Now);
            var totalEventsApplied = _eventStore.ApplyAllTo<CustomerQueue, CustomerQueueCreated, CustomerQueueSubscription>(_customerQueueRepository);
            WriteLine($"Total events applied for '{nameof(CustomerQueue)}': {totalEventsApplied}");
            WriteLine("Finished applying events " + DateTime.Now);
        }
    }
}