using System;
using static System.Guid;

namespace Tests.Views
{
    public static class CustomerQueueViewsTestValues
    {
        public static Guid CustomerQueueId = NewGuid();
        
        private static DateTime FromHour(int hour) => new DateTime(1, 1, 1, hour, 0, 0);
        public static readonly DateTime Hour9 = FromHour(9);
        public static readonly DateTime Hour10 = FromHour(10);
        public static readonly DateTime Hour11 = FromHour(11);
        public static readonly DateTime Hour12 = FromHour(12);
        public static readonly DateTime Hour14 = FromHour(14);
        public static readonly DateTime Hour16 = FromHour(16);
    }
}