using System;

namespace QuerySide.Views.QueueStatus
{
    public sealed class CounterStatus
    {
        public StatusInternal Status { get; }
        public int LastTicketNumber { get; }
        public DateTime LastTicketCallTimestamp { get; }
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

        public enum StatusInternal
        {
            Open,
            Closed,
            Fail
        }
    }
}