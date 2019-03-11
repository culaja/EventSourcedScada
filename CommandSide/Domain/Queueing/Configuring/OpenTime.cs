using System;
using System.Collections.Generic;
using Common;
using Common.Time;
using static Common.Time.TimeOfDay;

namespace CommandSide.Domain.Queueing.Configuring
{
    public sealed class OpenTime : ValueObject<OpenTime>
    {
        public DayOfWeek Day { get; }
        public TimeOfDay BeginTimestamp { get; }
        public TimeOfDay EndTimestamp { get; }

        public OpenTime(
            DayOfWeek day,
            TimeOfDay beginTimestamp,
            TimeOfDay endTimestamp)
        {
            Day = day;
            BeginTimestamp = beginTimestamp;
            EndTimestamp = endTimestamp;
        }
        
        public static OpenTime OpenTimeFrom(Maybe<string> maybeDayOfWeek, Maybe<string> maybeBeginTimestamp, Maybe<string> maybeEndTimestamp)
        {
            if (maybeDayOfWeek.HasValue && maybeBeginTimestamp.HasValue && maybeEndTimestamp.HasValue)
            {
                var beginTime = TimeOfDayFrom(maybeBeginTimestamp);
                var endTime = TimeOfDayFrom(maybeEndTimestamp);
                
                var isDayOfWeek = Enum.TryParse(maybeDayOfWeek.Value, out DayOfWeek dow);
                if (isDayOfWeek && beginTime.IsTimeBeforeAnother(endTime))
                {
                    return new OpenTime(dow, beginTime, endTime);
                }
            }
            throw new UnableToCreateOpenTimeException(maybeDayOfWeek, maybeBeginTimestamp, maybeEndTimestamp);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Day;
            yield return BeginTimestamp;
            yield return EndTimestamp;
        }

        public bool OverlapsWith(OpenTime ot)
        {
            if (this == ot) return false;
            if (Day != ot.Day) return false;
            if (BeginTimestamp.IsTimeEqualOrAfterAnother(ot.EndTimestamp)) return false;
            if (EndTimestamp.IsTimeEqualOrBeforeAnother(ot.BeginTimestamp)) return false;
            return true;
        }
    }
}