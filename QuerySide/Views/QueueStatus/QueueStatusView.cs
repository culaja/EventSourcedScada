using System;
using System.Collections.Generic;
using QuerySide.QueryCommon;

namespace QuerySide.Views.QueueStatus
{
    public sealed class QueueStatusView : SynchronizedView
    {
        public IReadOnlyList<WaitingCustomer> WaitingCustomers { get; } = new List<WaitingCustomer>();
        public IReadOnlyList<CounterStatus> CounterStatuses { get; } = new List<CounterStatus>();
        public int ExpectedWaitingTimeInSeconds { get; } = 0;
        public DateTime CurrentTime => DateTime.Now;
    }
}