using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.RemoteDomain.Commands;
using Common;
using Common.Messaging;
using static CommandSide.Domain.RemoteDomain.Remote;

namespace CommandSide.DomainServices.RemoteHandlers.CommandHandlers
{
    public sealed class AddRemoteHandler : CommandHandler<AddRemote>
    {
        private readonly IRemoteRepository _remoteRepository;

        public AddRemoteHandler(IRemoteRepository remoteRepository)
        {
            _remoteRepository = remoteRepository;
        }

        public override Result Handle(AddRemote c) => _remoteRepository
            .AddNew(NewRemoteFrom(c.RemoteId, c.RemoteName));
    }
}