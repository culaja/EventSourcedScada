using System;
using Common.Messaging;

namespace CommandSide.Domain.RemoteDomain.Commands
{
    public sealed class AddRemote : ICommand
    {
        public AddRemote(Guid remoteId)
        {
            RemoteId = remoteId;
        }

        public Guid RemoteId { get; }
    }
}