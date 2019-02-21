using System;
using Common.Messaging;

namespace Domain.Commands
{
    public sealed class TakeNextCustomer : ICommand
    {
        public CounterName CounterName { get; }
        public DateTime Timestamp { get; }

        public TakeNextCustomer(
            CounterName counterName,
            DateTime timestamp)
        {
            CounterName = counterName;
            Timestamp = timestamp;
        }
    }
}