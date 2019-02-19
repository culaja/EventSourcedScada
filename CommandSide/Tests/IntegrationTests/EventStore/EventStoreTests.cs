using System;
using System.Linq;
using FluentAssertions;
using Mongo2Go;
using MongoDbEventStore;
using Ports.EventStore;
using Tests.Specifications.CustomerQueueSpecifications;
using Xunit;
using static System.Guid;

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
           
        }
    }
}