using System;
using Common.Messaging;

namespace CommandSide.Domain.RemoteDomain.Commands
{
    public sealed class AddRemote : ICommand
    {
        public Guid RemoteId { get; }
        public RemoteName RemoteName { get; }

        public AddRemote(
            Guid remoteId,
            RemoteName remoteName)
        {
            RemoteId = remoteId;
            RemoteName = remoteName;
        }
    }
}