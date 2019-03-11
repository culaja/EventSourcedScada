using CommandSide.Domain.Queueing.Configuring;
using Common.Time;
using static Common.Time.TimeOfDay;

namespace CommandSide.Tests.UnitTests
{
    public static class DomainTestValues
    {
        public static TimeOfDay Time_24 = TimeOfDayFrom("1.00:00");
        public static TimeOfDay Time_09 = TimeOfDayFrom("09:00");
        public static TimeOfDay Time_18 = TimeOfDayFrom("18:00");

        public static OpenTime OpenTimeMonday = OpenTime.OpenTimeFrom("Monday", "09:00", "18:00");
        public static OpenTime OpenTimeMonday2 = OpenTime.OpenTimeFrom("Monday", "19:00", "20:00");
        public static OpenTime OpenTimeMondayOverlapping = OpenTime.OpenTimeFrom("Monday", "17:00", "20:00");
        public static OpenTime OpenTimeTuesday = OpenTime.OpenTimeFrom("Tuesday", "09:00", "18:00");
    }
}