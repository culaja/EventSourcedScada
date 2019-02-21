using System;
using Domain;
using Shared.CustomerQueue;
using static System.Guid;

namespace Tests
{
    public static class CustomerQueueTestValues
    {
        public static readonly Guid SingleCustomerQueueId = NewGuid();
        
        public static readonly CounterName CounterA_Name = "CounterA".ToCounterName();
        public static readonly DateTime CounterA_TakeNextCustomerTimestamp = DateTime.Today - TimeSpan.FromHours(10);

        public static readonly CounterName CounterB_Name = "CounterB".ToCounterName();
        
        public static readonly TicketId Ticket1_Id = NewGuid().ToTicketId();
        public static readonly int Ticket1_Number = 1;
        public static readonly DateTime Ticket1_PrintingTimestamp = DateTime.Today - TimeSpan.FromDays(1);
        public static readonly DateTime Ticket1_TakenTimestamp = DateTime.Today - TimeSpan.FromHours(5);
        public static readonly DateTime Ticket1_ServedTimestamp = DateTime.Today - TimeSpan.FromHours(4);
        
        public static readonly TicketId Ticket2_Id = NewGuid().ToTicketId();
        public static readonly int Ticket2_Number = 2;
        public static readonly DateTime Ticket2_PrintingTimestamp = DateTime.Today - TimeSpan.FromHours(4);
        public static readonly DateTime Ticket2_TakenTimestamp = DateTime.Today - TimeSpan.FromHours(3);
        public static readonly DateTime Ticket2_ServedTimestamp = DateTime.Today - TimeSpan.FromHours(2);
        
        public static readonly CustomerQueueCreated SingleCustomerQueueCreated = new CustomerQueueCreated(SingleCustomerQueueId);
        public static readonly CounterAdded CounterA_Added = new CounterAdded(SingleCustomerQueueId, CounterA_Name);
        public static readonly TicketAdded Ticket1_Added = new TicketAdded(SingleCustomerQueueId, Ticket1_Id, Ticket1_Number, Ticket1_PrintingTimestamp);
        public static readonly CustomerTaken CustomerWithTicket1_Taken = new CustomerTaken(SingleCustomerQueueId, CounterA_Name, Ticket1_Id, Ticket1_TakenTimestamp);
        public static readonly CustomerServed CustomerWithTicket1_Served = new CustomerServed(SingleCustomerQueueId, CounterA_Name, Ticket1_Id, Ticket1_ServedTimestamp);
        public static readonly CustomerRevoked CustomerWithTicket1_Revoked = new CustomerRevoked(SingleCustomerQueueId, CounterA_Name, Ticket1_Id);
    }
}