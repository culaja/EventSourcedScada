using System;
using Common.Time;
using static System.Guid;
using static Common.Time.TimeOfDay;

namespace Tests.Views
{
    public static class CustomerQueueViewsTestValues
    {
        public static Guid CustomerQueueId = NewGuid();
        
        public static readonly TimeOfDay Hour9 = TimeOfDayFromHour(9);
        public static readonly TimeOfDay Hour10 = TimeOfDayFromHour(10);
        public static readonly TimeOfDay Hour11 = TimeOfDayFromHour(11);
        public static readonly TimeOfDay Hour12 = TimeOfDayFromHour(12);
        public static readonly TimeOfDay Hour14 = TimeOfDayFromHour(14);
        public static readonly TimeOfDay Hour16 = TimeOfDayFromHour(16);
    }
}