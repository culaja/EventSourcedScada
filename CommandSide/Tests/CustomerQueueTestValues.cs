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

        public static readonly CounterName CounterB_Name = "CounterB".ToCounterName();
        
        public static readonly TicketId Ticket1_Id = NewGuid().ToTicketId();
        public static readonly int Ticket1_Number = 1;
        
        public static readonly TicketId Ticket2_Id = NewGuid().ToTicketId();
        public static readonly int Ticket2_Number = 2;
        
        public static readonly CustomerQueueCreated SingleCustomerQueueCreated = new CustomerQueueCreated(SingleCustomerQueueId);
        public static readonly CounterAdded CounterA_Added = new CounterAdded(SingleCustomerQueueId, CounterA_Name);
        public static readonly TicketAdded Ticket1_Added = new TicketAdded(SingleCustomerQueueId, Ticket1_Id, Ticket1_Number);
        public static readonly CustomerTaken CustomerWithTicket1_Taken = new CustomerTaken(SingleCustomerQueueId, CounterA_Name, Ticket1_Id);
        public static readonly CustomerServed CustomerWithTicket1_Served = new CustomerServed(SingleCustomerQueueId, CounterA_Name, Ticket1_Id);
        public static readonly CustomerRevoked CustomerWithTicket1_Revoked = new CustomerRevoked(SingleCustomerQueueId, CounterA_Name, Ticket1_Id);
    }
}