using System;
using System.Linq;
using FluentAssertions;
using Mongo2Go;
using MongoDbEventStore;
using Ports.EventStore;
using Shared.CustomerQueue;
using Tests.Specifications.CustomerQueueSpecifications;
using Xunit;
using static System.Guid;
using static Tests.CustomerQueueTestValues;

namespace Tests.IntegrationTests.EventStore
{
    public sealed class EventStoreTests : IDisposable
    {
        private readonly MongoDbRunner _runner = MongoDbRunner.Start();
        private readonly IEventStore _eventStore;

        public EventStoreTests()
        {
            var connectionString = _runner.ConnectionString;
            var databaseName = NewGuid().ToString();
            _eventStore = new MongoDbEventStore.EventStore(new DatabaseContext(connectionString, databaseName));
        }

        public void Dispose()
        {
            _runner.Dispose();
        }
        
        [Fact]
        public void _1()
        {
            _eventStore.Append(SingleCustomerQueueCreated);
            _eventStore.Append(CounterA_Added);
            _eventStore.Append(Ticket1_Added);
            _eventStore.Append(CustomerWithTicket1_Added);
            _eventStore.Append(CustomerWithTicket1_Served);
           
            var allEvents =  _eventStore.LoadAllFor<CustomerQueueSubscription>().ToList();

            allEvents.Should().BeEquivalentTo(
                SingleCustomerQueueCreated,
                CounterA_Added,
                Ticket1_Added,
                CustomerWithTicket1_Added,
                CustomerWithTicket1_Served);
        }
    }
}