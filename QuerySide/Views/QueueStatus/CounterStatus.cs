using System;
using Common;
using static System.DateTime;
using static Common.Nothing;
using static QuerySide.Views.QueueStatus.CounterStatus.StatusInternal;

namespace QuerySide.Views.QueueStatus
{
    public sealed class CounterStatus
    {
        public StatusInternal Status { get; private set; }
        public int LastTicketNumber { get; private set;}
        public DateTime LastTicketCallTimestamp { get; private set; }
        public string AliasName { get; }
        public int CounterNumber { get; }

        public CounterStatus(
            StatusInternal status,
            int lastTicketNumber,
            DateTime lastTicketCallTimestamp,
            string aliasName,
            int counterNumber)
        {
            Status = status;
            LastTicketNumber = lastTicketNumber;
            LastTicketCallTimestamp = lastTicketCallTimestamp;
            AliasName = aliasName;
            CounterNumber = counterNumber;
        }
        
        public static CounterStatus NewCounterWith(int number, string alias) => new CounterStatus(
            Closed,
            0, 
            MinValue, 
            alias,
            number);

        public Nothing SetServingTicket(
            int ticketNumber,
            DateTime lastTicketCallTimestamp)
        {
            LastTicketNumber = ticketNumber;
            LastTicketCallTimestamp = lastTicketCallTimestamp;
            return NotAtAll;
        }

        public Nothing SetCounterOpened()
        {
            Status = Open;
            return NotAtAll;
        }

        public Nothing SetCounterClosed()
        {
            Status = Closed;
            return NotAtAll;
        }

        public enum StatusInternal
        {
            Open,
            Closed,
            Fail
        }
    }
}