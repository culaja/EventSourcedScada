using System;
using System.Collections.Generic;
using Common;
using Common.Time;

namespace CommandSide.Domain.Queueing.Configuring
{
    public sealed class OpenTime : ValueObject<OpenTime>
    {
        public DayOfWeek Day { get; }
        public TimeOfDay BeginTimeOfDay { get; }
        public TimeOfDay EndTimeOfDay { get; }

        public OpenTime(
            DayOfWeek day,
            TimeOfDay beginTimestamp,
            TimeOfDay endTimestamp)
        {
            Day = day;
            BeginTimeOfDay = beginTimestamp;
            EndTimeOfDay = endTimestamp;
        }

        public static OpenTime OpenTimeFrom(DayOfWeek dayOfWeek, TimeOfDay beginTimeOfDay, TimeOfDay endTimeOfDay)
        {
            if (beginTimeOfDay.IsTimeEqualOrAfterAnother(endTimeOfDay))
            {
                throw new BeginTimeNeedsToBeBeforeEndTimeException(beginTimeOfDay, endTimeOfDay);
            }
            
            return new OpenTime(dayOfWeek, beginTimeOfDay, endTimeOfDay);
        }

        public bool OverlapsWith(OpenTime ot)
        {
            if (this == ot) return false;
            if (Day != ot.Day) return false;
            if (BeginTimeOfDay.IsTimeEqualOrAfterAnother(ot.EndTimeOfDay)) return false;
            if (EndTimeOfDay.IsTimeEqualOrBeforeAnother(ot.BeginTimeOfDay)) return false;
            return true;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Day;
            yield return BeginTimeOfDay;
            yield return EndTimeOfDay;
        }

        public override string ToString() => $"{nameof(Day)}: {Day.ToString()}, {nameof(BeginTimeOfDay)}: {BeginTimeOfDay}, {nameof(EndTimeOfDay)}: {EndTimeOfDay}";
    }
}