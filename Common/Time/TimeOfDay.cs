using System;
using System.Collections.Generic;
using static System.TimeSpan;

namespace Common.Time
{
    public sealed class TimeOfDay : ValueObject<TimeOfDay>
    {
        public TimeSpan Timespan { get; }

        public TimeOfDay(TimeSpan timespan)
        {
            if (timespan > FromDays(1))
            {
                throw new TimeOfDayCantBeGreaterThanDayException(timespan);
            }

            Timespan = timespan;
        }

        public static TimeOfDay TimeOfDayFrom(TimeSpan timeSpan)
        {
            return new TimeOfDay(timeSpan);
        }

        public static TimeOfDay TimeOfDayFromHour(int hour) => new TimeOfDay(FromHours(hour));

        public static TimeOfDay TimeOfDayFrom(DateTime dateTime) => new TimeOfDay(dateTime.TimeOfDay);

        public bool IsTimeBeforeAnother(TimeOfDay otherTime) => Timespan.CompareTo(otherTime.Timespan) < 0;

        public bool IsTimeAfterAnother(TimeOfDay otherTime) => Timespan.CompareTo(otherTime.Timespan) > 0;

        public bool IsTimeEqualOrAfterAnother(TimeOfDay otherTime) => Timespan.CompareTo(otherTime.Timespan) >= 0;

        public bool IsTimeEqualOrBeforeAnother(TimeOfDay otherTime) => Timespan.CompareTo(otherTime.Timespan) <= 0;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Timespan;
        }

        public override string ToString() => Timespan.ToString();

        public static implicit operator TimeSpan(TimeOfDay timeOfDay) => timeOfDay.Timespan;

        public static bool operator <=(TimeOfDay a, TimeOfDay b) => a.Timespan <= b.Timespan;
        public static bool operator >=(TimeOfDay a, TimeOfDay b) => a.Timespan >= b.Timespan;
    }
}