using System;
using System.Collections.Generic;
using static System.String;
using static System.TimeSpan;

namespace Common.Time
{
    public sealed class TimeOfDay : ValueObject<TimeOfDay>
    {
        public TimeSpan Time { get; }

        public TimeOfDay(TimeSpan time)
        {
            Time = time;
        }

        public static TimeOfDay TimeOfDayFrom(Maybe<string> maybeTime)
        {
            if (!TryParse(maybeTime.Unwrap(Empty), out var timespan))
            {
                throw new TimeOfDayFormatException(maybeTime); 
            }

            if (timespan > TimeSpan.FromDays(1))
            {
                throw new TimeOfDayCantBeGreaterThanDayException(timespan);
            }
                
            return new TimeOfDay(timespan);
        }
        
        public static TimeOfDay TimeOfDayFromHour(int hour) => new TimeOfDay(FromHours(hour));

        public bool IsTimeBeforeAnother(TimeOfDay otherTime) => Time.CompareTo(otherTime.Time) < 0;
        
        public bool IsTimeEqualOrAfterAnother(TimeOfDay otherTime) => Time.CompareTo(otherTime.Time) >= 0;
        
        public bool IsTimeEqualOrBeforeAnother(TimeOfDay otherTime) => Time.CompareTo(otherTime.Time) <= 0;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Time;
        }

        public override string ToString() => Time.ToString();
    }
}