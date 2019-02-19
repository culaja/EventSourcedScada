using System;
using Common.Messaging;

namespace Domain.Commands
{
    public sealed class TakeNextCustomer : ICommand
    {
        public Guid CounterId { get; }
        public DateTime Timestamp { get; }

        public TakeNextCustomer(
            Guid counterId,
            DateTime timestamp)
        {
            CounterId = counterId;
            Timestamp = timestamp;
        }
    }
}