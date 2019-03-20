using System;
using Common.Time;

namespace QuerySide.Tests.Views
{
    public static class CustomerQueueViewsTestValues
    {
        public static Guid TicketIssuerId = Guid.NewGuid();
        public static Guid CustomerQueueId = Guid.NewGuid();
        
        public static readonly TimeOfDay Hour9 = TimeOfDay.TimeOfDayFromHour(9);
        public static readonly TimeOfDay Hour10 = TimeOfDay.TimeOfDayFromHour(10);
        public static readonly TimeOfDay Hour11 = TimeOfDay.TimeOfDayFromHour(11);
        public static readonly TimeOfDay Hour12 = TimeOfDay.TimeOfDayFromHour(12);
        public static readonly TimeOfDay Hour14 = TimeOfDay.TimeOfDayFromHour(14);
        public static readonly TimeOfDay Hour16 = TimeOfDay.TimeOfDayFromHour(16);

        public static int Counter1Id = 1;

        public static Guid Ticket1Id = Guid.NewGuid();
        public static Guid Ticket2Id = Guid.NewGuid();
        public static Guid Ticket3Id = Guid.NewGuid();
        public static Guid Ticket4Id = Guid.NewGuid();
        public static Guid Ticket5Id = Guid.NewGuid();
        public static Guid Ticket6Id = Guid.NewGuid();
        
        public static int Ticket1Number = 1;
        public static int Ticket2Number = 2;
        public static int Ticket3Number = 3;
        public static int Ticket4Number = 4;
        public static int Ticket5Number = 5;
        public static int Ticket6Number = 6;
    }
}