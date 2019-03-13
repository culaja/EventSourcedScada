using System;
using System.Collections.Generic;
using System.Linq;
using Common.Messaging;
using CommonAdapters.MongoDbEventStore;
using FluentAssertions;
using Mongo2Go;
using Ports.EventStore;
using Shared.TicketIssuer;
using Xunit;
using static CommandSide.Tests.Specifications.TicketIssuerSpecifications.TicketIssuerTestValues;

namespace CommandSide.Tests.IntegrationTests.EventStore
{
    public sealed class TicketIssuerStreamTests : IDisposable
    {
        private readonly MongoDbRunner _runner = MongoDbRunner.Start();
        private readonly IEventStore _eventStore;

        public TicketIssuerStreamTests()
        {
            var connectionString = _runner.ConnectionString;
            var databaseName = Guid.NewGuid().ToString();
            _eventStore = new CommonAdapters.MongoDbEventStore.EventStore(new DatabaseContext(connectionString, databaseName));
        }

        public void Dispose()
        {
            _runner.Dispose();
        }
        
        [Fact]
        public void _1()
        {
            _eventStore.Append(SingleTicketIssuerCreated.SetAnyVersionAndTimestamp());
            _eventStore.Append(Monday9To12Added.SetAnyVersionAndTimestamp());
            _eventStore.Append(Monday9To12Removed.SetAnyVersionAndTimestamp());
            
           
            var allEvents =  _eventStore.LoadAllFor<TicketIssuerSubscription>().ToList();

            ListsAreEquivalent(allEvents, 
                SingleTicketIssuerCreated,
                Monday9To12Added,
                Monday9To12Removed);
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