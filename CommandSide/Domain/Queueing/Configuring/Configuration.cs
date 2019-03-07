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
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CountersDetails;
            yield return OpenTimes;
        }
    }
}