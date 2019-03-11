using System;
using System.Collections.Generic;
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
            if (maybeTime.HasValue)
            {
                var isValidTime = TryParse(maybeTime.Value, out TimeSpan time);
                if (isValidTime)
                {
                    return new TimeOfDay(time);
                }
            }
            throw new UnableToCreateTimeOfDayException(maybeTime);   
        }
        
        public static TimeOfDay TimeOfDayFromHour(int hour) => new TimeOfDay(FromHours(hour));

        public bool IsTimeBeforeAnother(TimeOfDay otherTime) => Time.CompareTo(otherTime.Time) < 0;
        
        public bool IsTimeEqualOrAfterAnother(TimeOfDay otherTime) => Time.CompareTo(otherTime.Time) >= 0;
        
        public bool IsTimeEqualOrBeforeAnother(TimeOfDay otherTime) => Time.CompareTo(otherTime.Time) <= 0;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Time;
        }
    }
}