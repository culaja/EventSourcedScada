using System.Collections.Generic;
using Common;
using static CommandSide.Domain.Queueing.Configuring.CountersDetails;
using static CommandSide.Domain.Queueing.Configuring.OpenTimes;

namespace CommandSide.Domain.Queueing.Configuring
{
    public sealed class Configuration : ValueObject<Configuration>
    {
        public CountersDetails CountersDetails { get; }
        public OpenTimes OpenTimes { get; }

        public Configuration(
            CountersDetails countersDetails,
            OpenTimes openTimes)
        {
            CountersDetails = countersDetails;
            OpenTimes = openTimes;
        }
        
        public static Configuration EmptyConfiguration => new Configuration(EmptyCountersDetails, NoOpenTimes);
        
        public Configuration AddOpenTime(OpenTime openTime) => new Configuration(CountersDetails, OpenTimes.Add(openTime));

        public CountersDetails IsolateCountersToAdd(IReadOnlyList<CounterId> counterIds) =>
            CountersDetails.IsolateCountersToAdd(counterIds);

        public IReadOnlyList<CounterId> IsolateCounterIdsToRemove(IReadOnlyList<CounterId> counterIds) =>
            CountersDetails.IsolateCounterIdsToRemove(counterIds);
        
        public OpenTimes IsolateOpenTimesToAdd(OpenTimes currentOpenTimes) =>
            OpenTimes.IsolateOpenTimesToAdd(currentOpenTimes);

        public OpenTimes IsolateOpenTimesToRemove(OpenTimes currentOpenTimes) =>
            OpenTimes.IsolateOpenTimesToRemove(currentOpenTimes);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CountersDetails;
            yield return OpenTimes;
        }

        public bool ContainsOverlappingOpenTimeWith(OpenTimes currentOpenTimes) => 
            OpenTimes.ContainsOverlappingOpenTimeWith(currentOpenTimes);
    }
}