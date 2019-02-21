using System;
using Common.Messaging;

namespace Domain.Commands
{
    public sealed class AddCustomerQueue : ICommand
    {
        public Guid AggregateRootId { get; }

        public AddCustomerQueue(Guid aggregateRootId)
        {
            AggregateRootId = aggregateRootId;
        }
    }
}