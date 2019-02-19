using System;
using Common.Messaging;

namespace Domain.Commands
{
    public sealed class TakeNextCustomer : ICommand
    {
        public Guid CounterId { get; }

        public TakeNextCustomer(Guid counterId)
        {
            CounterId = counterId;
        }
    }
}