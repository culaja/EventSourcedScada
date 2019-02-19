using System;
using System.Collections;
using static System.DateTime;
using static System.Guid;
using static System.TimeSpan;

namespace Tests.Specifications.CustomerQueueSpecifications
{
    public static class CustomerQueueTestValues
    {
        public static readonly Guid CounterA_Id = NewGuid();
        public static readonly string CounterA_Name = "CounterA";
        public static readonly DateTime CounterA_TakeNextCustomerTimestamp = Today - FromHours(10);

        public static readonly Guid CounterB_Id = NewGuid();
        public static readonly string CounterB_Name = "CounterB";
        
        public static readonly Guid Ticket1_Id = NewGuid();
        public static readonly int Ticket1_Number = 1;
        public static readonly DateTime Ticket1_PrintingTimestamp = Today - FromDays(1);
        public static readonly DateTime Ticket1_TakenTimestamp = Today - FromHours(5);
        public static readonly DateTime Ticket1_ServedTimestamp = Today - FromHours(4);
        
        public static readonly Guid Ticket2_Id = NewGuid();
        public static readonly int Ticket2_Number = 2;
        public static readonly DateTime Ticket2_PrintingTimestamp = Today - FromHours(4);
        public static readonly DateTime Ticket2_TakenTimestamp = Today - FromHours(3);
        public static readonly DateTime Ticket2_ServedTimestamp = Today - FromHours(2);
    }
}