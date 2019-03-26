using System;
using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using QuerySide.QueryCommon;
using Shared.CustomerQueue.Events;
using static System.DateTime;
using static Common.Nothing;
using static QuerySide.Views.QueueStatus.CounterStatusDetails.StatusInternal;

namespace QuerySide.Views.QueueStatus
{
    public sealed class CounterStatusDetails :
        IHandle<CounterOpened>,
        IHandle<CounterClosed>,
        IHandle<CounterNameChanged>
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusInternal CounterStatus { get; private set; }
        public int LastTicketNumber { get; private set; }
        public DateTime LastTicketCallTimestamp { get; private set; }
        public string AliasName { get; private set; }
        public int CounterNumber { get; }

        public CounterStatusDetails(
            StatusInternal status,
            int lastTicketNumber,
            DateTime lastTicketCallTimestamp,
            string aliasName,
            int counterNumber)
        {
            CounterStatus = status;
            LastTicketNumber = lastTicketNumber;
            LastTicketCallTimestamp = lastTicketCallTimestamp;
            AliasName = aliasName;
            CounterNumber = counterNumber;
        }

        public static CounterStatusDetails NewCounterStatusDetailsWith(int number, string alias) => new CounterStatusDetails(
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
            CounterStatus = Open;
        }

        public void Handle(CounterClosed _)
        {
            CounterStatus = Closed;
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