using System;
using Common;
using QuerySide.QueryCommon;
using Shared.CustomerQueue.Events;
using static System.DateTime;
using static Common.Nothing;
using static QuerySide.Views.QueueStatus.CounterStatus.StatusInternal;

namespace QuerySide.Views.QueueStatus
{
    public sealed class CounterStatus :
        IHandle<CounterOpened>,
        IHandle<CounterClosed>,
        IHandle<CounterNameChanged>
    {
        public StatusInternal Status { get; private set; }
        public int LastTicketNumber { get; private set; }
        public DateTime LastTicketCallTimestamp { get; private set; }
        public string AliasName { get; private set; }
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

        public void Handle(CounterOpened _)
        {
            Status = Open;
        }

        public void Handle(CounterClosed _)
        {
            Status = Closed;
        }

        public void Handle(CounterNameChanged e)
        {
            AliasName = e.NewCounterName;
        }

        public enum StatusInternal
        {
            Open,
            Closed,
            Fail
        }
    }
}