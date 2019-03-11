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
        
        public static Result<OpenTime> OpenTimeFrom(Maybe<string> maybeDayOfWeek, Maybe<string> maybeBeginTimestamp, Maybe<string> maybeEndTimestamp)
        {
            if (maybeDayOfWeek.HasValue && maybeBeginTimestamp.HasValue && maybeEndTimestamp.HasValue)
            {
                var beginTime = TimeOfDayFrom(maybeBeginTimestamp);
                var endTime = TimeOfDayFrom(maybeEndTimestamp);
                
                var isDayOfWeek = Enum.TryParse(maybeDayOfWeek.Value, out DayOfWeek day);
                if (isDayOfWeek && beginTime.IsSuccess && endTime.IsSuccess && beginTime.Value.IsTimeBeforeAnother(endTime.Value))
                {
                    return Result.Ok(new OpenTime(day, beginTime.Value, endTime.Value));
                }
            }
            return Result.Fail<OpenTime>($"Unable to create open time with provided arguments: DayOfWeek '{maybeDayOfWeek}'," +
                                         $" BeginTimestamp '{maybeBeginTimestamp}', EndTimestamp '{maybeEndTimestamp}'");
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
            if (ot.BeginTimestamp.IsTimeEqualOrAfterAnother(EndTimestamp)) return false;
            return true;
        }
    }
}