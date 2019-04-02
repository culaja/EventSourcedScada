using System;
using Common;
using Shared.Remote.Events;

namespace CommandSide.Domain.RemoteDomain
{
    public sealed class Remote : AggregateRoot
    {
        public Remote(Guid id) : base(id)
        {
        }

        public static Remote NewRemoteFrom(
            Guid id)
        {
            var customerQueue = new Remote(id);
            customerQueue.ApplyChange(new RemoteCreated(customerQueue.Id));
            return customerQueue;
        }

        private Remote Apply(RemoteCreated _) => this;
    }
}