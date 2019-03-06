using System;
using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing.Configuring
{
    public sealed class OpenTime : ValueObject<OpenTime>
    {
        public DayOfWeek Day { get; }
        public DateTime BeginTimestamp { get; }
        public DateTime EndTimestamp { get; }

        public OpenTime(
            DayOfWeek day,
            DateTime beginTimestamp,
            DateTime endTimestamp)
        {
            Day = day;
            BeginTimestamp = beginTimestamp;
            EndTimestamp = endTimestamp;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Day;
            yield return BeginTimestamp;
            yield return EndTimestamp;
        }
    }
}