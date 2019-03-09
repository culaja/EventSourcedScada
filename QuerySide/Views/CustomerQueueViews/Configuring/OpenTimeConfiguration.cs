using System;
using System.Collections.Generic;
using Common;
using Common.Time;

namespace QuerySide.Views.CustomerQueueViews.Configuring
{
    public sealed class OpenTimeConfiguration : ValueObject<OpenTimeConfiguration>
    {
        public DayOfWeek Day { get; }
        public TimeOfDay BeginTimestamp { get; }
        public TimeOfDay EndTimestamp { get; }

        public OpenTimeConfiguration(
            DayOfWeek day,
            TimeOfDay beginTimestamp,
            TimeOfDay endTimestamp)
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