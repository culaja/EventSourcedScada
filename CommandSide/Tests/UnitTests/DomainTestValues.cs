using System;
using CommandSide.Domain.Queueing.Configuring;
using Common.Time;
using static System.DayOfWeek;
using static Common.Time.TimeOfDay;

namespace CommandSide.Tests.UnitTests
{
    public static class DomainTestValues
    {
        public static readonly TimeOfDay Time_24 = TimeOfDayFrom(new TimeSpan(1, 0 ,0, 0));
        public static readonly TimeOfDay Time_09 = TimeOfDayFrom(new TimeSpan(0, 9 ,0, 0));
        public static readonly TimeOfDay Time_17 = TimeOfDayFrom(new TimeSpan(0, 17 ,0, 0));
        public static readonly TimeOfDay Time_18 = TimeOfDayFrom(new TimeSpan(0, 18 ,0, 0));
        public static readonly TimeOfDay Time_19 = TimeOfDayFrom(new TimeSpan(0, 19 ,0, 0));
        public static readonly TimeOfDay Time_20 = TimeOfDayFrom(new TimeSpan(0, 20 ,0, 0));

        public static readonly OpenTime OpenTimeMonday = OpenTime.OpenTimeFrom(Monday, Time_09, Time_18);
        public static readonly OpenTime OpenTimeMonday2 = OpenTime.OpenTimeFrom(Monday, Time_19, Time_20);
        public static readonly OpenTime OpenTimeMondayOverlapping = OpenTime.OpenTimeFrom(Monday, Time_17, Time_20);
        public static readonly OpenTime OpenTimeTuesday = OpenTime.OpenTimeFrom(Tuesday, Time_09, Time_18);
    }
}