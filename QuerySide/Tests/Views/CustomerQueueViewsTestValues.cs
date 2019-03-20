using System;
using Common.Time;
using static System.DateTime;

namespace QuerySide.Tests.Views
{
    public static class CustomerQueueViewsTestValues
    {
        public static readonly Guid TicketIssuerId = Guid.NewGuid();
        public static readonly Guid CustomerQueueId = Guid.NewGuid();
        
        public static readonly TimeOfDay Hour9 = TimeOfDay.TimeOfDayFromHour(9);
        public static readonly TimeOfDay Hour10 = TimeOfDay.TimeOfDayFromHour(10);
        public static readonly TimeOfDay Hour11 = TimeOfDay.TimeOfDayFromHour(11);
        public static readonly TimeOfDay Hour12 = TimeOfDay.TimeOfDayFromHour(12);
        public static readonly TimeOfDay Hour14 = TimeOfDay.TimeOfDayFromHour(14);
        public static readonly TimeOfDay Hour16 = TimeOfDay.TimeOfDayFromHour(16);

        public static readonly int Counter1Id = 1;
        public static readonly int Counter2Id = 2;
        public static readonly int Counter3Id = 3;
        
        public static readonly string Counter1Name = "C1";
        public static readonly string Counter2Name = "C2";
        public static readonly string Counter3Name = "C3";

        public static readonly string Counter2ChangedName = "C2 XXX";

        public static readonly Guid Ticket1Id = Guid.NewGuid();
        public static readonly Guid Ticket2Id = Guid.NewGuid();
        public static readonly Guid Ticket3Id = Guid.NewGuid();
        public static readonly Guid Ticket4Id = Guid.NewGuid();
        public static readonly Guid OutOfLineTicket10kId = Guid.NewGuid();
        
        public static readonly int Ticket1Number = 1;
        public static readonly int Ticket2Number = 2;
        public static readonly int Ticket3Number = 3;
        public static readonly int Ticket4Number = 4;
        public static readonly int OutOfLineTicket10kNumber = 10000;
        
        public static readonly DateTime Ticket1IssuedTimestamp = UtcNow;
        public static readonly DateTime Ticket2IssuedTimestamp = UtcNow;
        public static readonly DateTime Ticket3IssuedTimestamp = UtcNow;
        public static readonly DateTime Ticket4IssuedTimestamp = UtcNow;
        public static readonly DateTime OutOfLineTicket10kIdIssuedTimestamp = UtcNow;
        
        public static readonly DateTime Ticket1AssignedTimestamp = UtcNow;
        public static readonly DateTime OutOfLineTicket10kIdAssignedTimestamp = UtcNow;
        
        public static readonly DateTime Ticket1ServedTimestamp = UtcNow;
    }
}