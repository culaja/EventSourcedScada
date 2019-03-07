using System;
using System.Collections.Generic;
using Common;

namespace QuerySide.Views.CustomerQueueViews.Configuring
{
    internal sealed class OpenTimeConfiguration : ValueObject<OpenTimeConfiguration>
    {
        public DayOfWeek Day { get; }
        public DateTime BeginTimestamp { get; }
        public DateTime EndTimestamp { get; }

        public OpenTimeConfiguration(
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