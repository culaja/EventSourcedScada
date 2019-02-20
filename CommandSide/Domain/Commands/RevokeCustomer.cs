using System;
using Common.Messaging;

namespace Domain.Commands
{
    public sealed class RevokeCustomer : ICommand
    {
        public Guid CounterId { get; }

        public RevokeCustomer(
            Guid counterId)
        {
            CounterId = counterId;
        }
    }
}