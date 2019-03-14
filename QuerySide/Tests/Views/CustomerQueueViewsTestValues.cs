using System;
using Common.Time;
using static System.Guid;
using static Common.Time.TimeOfDay;

namespace Tests.Views
{
    public static class CustomerQueueViewsTestValues
    {
        public static Guid TicketIssuerId = NewGuid();
        public static Guid CustomerQueueId = NewGuid();
        
        public static readonly TimeOfDay Hour9 = TimeOfDayFromHour(9);
        public static readonly TimeOfDay Hour10 = TimeOfDayFromHour(10);
        public static readonly TimeOfDay Hour11 = TimeOfDayFromHour(11);
        public static readonly TimeOfDay Hour12 = TimeOfDayFromHour(12);
        public static readonly TimeOfDay Hour14 = TimeOfDayFromHour(14);
        public static readonly TimeOfDay Hour16 = TimeOfDayFromHour(16);

        public static int Counter1Id = 1;

        public static Guid Ticket1Id = NewGuid();
        public static Guid Ticket2Id = NewGuid();
        public static Guid Ticket3Id = NewGuid();
        public static Guid Ticket4Id = NewGuid();
        public static Guid Ticket5Id = NewGuid();
        public static Guid Ticket6Id = NewGuid();
        
        public static int Ticket1Number = 1;
        public static int Ticket2Number = 2;
        public static int Ticket3Number = 3;
        public static int Ticket4Number = 4;
        public static int Ticket5Number = 5;
        public static int Ticket6Number = 6;
    }
}