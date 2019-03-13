using System;
using System.Collections.Generic;
using Common;

namespace QuerySide.Views.CustomerQueueViews.Configuring
{
    public sealed class OpenTimeConfiguration : ValueObject<OpenTimeConfiguration>
    {
        public DayOfWeek Day { get; }
        public TimeSpan BeginTimestamp { get; }
        public TimeSpan EndTimestamp { get; }

        public OpenTimeConfiguration(
            DayOfWeek day,
            TimeSpan beginTimestamp,
            TimeSpan endTimestamp)
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