using System;
using Common.Messaging;

namespace CommandSide.Domain.Commands
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