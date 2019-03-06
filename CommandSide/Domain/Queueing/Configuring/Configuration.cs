using System.Collections.Generic;
using Common;

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
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CountersDetails;
            yield return OpenTimes;
        }
    }
}