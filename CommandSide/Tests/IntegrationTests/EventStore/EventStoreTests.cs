using System;
using System.Collections.Generic;
using System.Linq;
using Common.Messaging;
using FluentAssertions;
using Mongo2Go;
using MongoDbEventStore;
using Ports.EventStore;
using Shared.CustomerQueue;
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
            _eventStore.Append(SingleCustomerQueueCreated.SetAnyVersionAndTimestamp());
            _eventStore.Append(CounterA_Added.SetAnyVersionAndTimestamp());
            _eventStore.Append(Ticket1_Added.SetAnyVersionAndTimestamp());
            _eventStore.Append(CustomerWithTicket1_Taken.SetAnyVersionAndTimestamp());
            _eventStore.Append(CustomerWithTicket1_Served.SetAnyVersionAndTimestamp());
            _eventStore.Append(CustomerWithTicket1_Revoked.SetAnyVersionAndTimestamp());
           
            var allEvents =  _eventStore.LoadAllFor<CustomerQueueSubscription>().ToList();

            ListsAreEquivalent(allEvents, 
                SingleCustomerQueueCreated,
                CounterA_Added,
                Ticket1_Added,
                CustomerWithTicket1_Taken,
                CustomerWithTicket1_Served,
                CustomerWithTicket1_Revoked);
        }

        private static void ListsAreEquivalent(IReadOnlyList<IDomainEvent> a, params IDomainEvent[] b)
        {
            a.Should().BeEquivalentTo(b);
            for (var i = 0; i < a.Count; ++i)
            {
                a[i].Version.Should().Be(b[i].Version);
                a[i].Number.Should().Be(b[i].Number);
                a[i].Timestamp.Should().Be(b[i].Timestamp);
            }
        }
    }
}