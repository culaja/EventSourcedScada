using System;
using Common.Messaging;

namespace Domain.Commands
{
    public sealed class TakeNextCustomer : ICommand
    {
        public CounterName CounterName { get; }

        public TakeNextCustomer(
            CounterName counterName)
        {
            CounterName = counterName;
        }
    }
}