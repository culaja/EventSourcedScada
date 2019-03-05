using System.Collections.Concurrent;

namespace MongoDbEventStore
{
    public sealed class AggregateEventNumberTracker
    {
        private readonly ConcurrentDictionary<string, ulong> _eventNumberPerAggregateDictionary = new ConcurrentDictionary<string, ulong>();

        public ulong AllocateNextEventNumberFor(string aggregateName)
        {
            if (!_eventNumberPerAggregateDictionary.TryGetValue(aggregateName, out var eventNumber))
            {
                eventNumber = 0;
                _eventNumberPerAggregateDictionary[aggregateName] = eventNumber;
            }

            _eventNumberPerAggregateDictionary[aggregateName] = ++eventNumber;
            return eventNumber;
        }

        public void UpdateEventNumberFor(string aggregateName, ulong newEventNumber)
        {
            _eventNumberPerAggregateDictionary[aggregateName] = newEventNumber;
        }
    }
}