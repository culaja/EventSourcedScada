using System;
using Common.Messaging;

namespace Domain.Commands
{
    public sealed class AddCounter : ICommand
    {
        public Guid CounterId { get; }
        public string CounterName { get; }

        public AddCounter(Guid counterId, string counterName)
        {
            CounterId = counterId;
            CounterName = counterName;
        }
    }
}