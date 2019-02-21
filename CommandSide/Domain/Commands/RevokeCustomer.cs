using System;
using Common.Messaging;

namespace Domain.Commands
{
    public sealed class RevokeCustomer : ICommand
    {
        public CounterName CounterName { get; }

        public RevokeCustomer(
            CounterName counterName)
        {
            CounterName = counterName;
        }
    }
}