using System;
using System.Collections.Generic;
using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace QuerySide.Views.Configuring
{
    public sealed class OpenTimeConfiguration : ValueObject<OpenTimeConfiguration>
    {
        [JsonConverter(typeof(StringEnumConverter))]
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