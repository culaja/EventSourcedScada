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

        public static Configuration ConfigurationFrom(CountersDetails countersDetails, OpenTimes openTimes) 
            => new Configuration(countersDetails, openTimes);

        public CountersDetails IsolateCountersToAdd(CountersDetails countersDetails) =>
            CountersDetails.IsolateCountersToAdd(countersDetails);

        public IReadOnlyList<CounterId> IsolateCounterIdsToRemove(CountersDetails countersDetails) =>
            CountersDetails.IsolateCounterIdsToRemove(countersDetails);

        public CountersDetails IsolateCountersDetailsWhereNameChanged(CountersDetails countersDetails) =>
            CountersDetails.IsolateCountersDetailsWhereNameDiffers(countersDetails);

        public OpenTimes IsolateOpenTimesToAdd(OpenTimes currentOpenTimes) =>
            OpenTimes.IsolateOpenTimesToAdd(currentOpenTimes);

        public OpenTimes IsolateOpenTimesToRemove(OpenTimes currentOpenTimes) =>
            OpenTimes.IsolateOpenTimesToRemove(currentOpenTimes);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CountersDetails;
            yield return OpenTimes;
        }
    }
}