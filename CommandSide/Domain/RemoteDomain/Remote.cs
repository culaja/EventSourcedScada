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
            var remote = new Remote(id);
            remote.ApplyChange(new RemoteCreated(remote.Id));
            return remote;
        }

        private Remote Apply(RemoteCreated _) => this;
    }
}