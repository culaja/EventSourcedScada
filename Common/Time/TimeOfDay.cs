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

        public static Result<TimeOfDay> TimeOfDayFrom(Maybe<string> maybeTime)
        {
            if (maybeTime.HasValue)
            {
                var isValidTime = TryParse(maybeTime.Value, out TimeSpan time);
                if (isValidTime)
                {
                    return Result.Ok(new TimeOfDay(time));
                }
            }
            return Result.Fail<TimeOfDay>($"Not able to create time of day with provided time '{maybeTime}'");
        }
        
        public static TimeOfDay TimeOfDayFromHour(int hour) => new TimeOfDay(FromHours(hour));

        public bool IsTimeBeforeAnother(TimeOfDay otherTime) => Time.CompareTo(otherTime.Time) < 0;

        public bool IsTimeEqualOrAfterAnother(TimeOfDay otherTime) => Time.CompareTo(otherTime.Time) >= 0;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Time;
        }
    }
}