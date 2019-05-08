using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.RemoteDomain.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.RemoteHandlers.CommandHandlers
{
    public sealed class AddAnalogHandler : CommandHandler<AddAnalog>
    {
        private readonly IRemoteRepository _remoteRepository;

        public AddAnalogHandler(IRemoteRepository remoteRepository)
        {
            _remoteRepository = remoteRepository;
        }

        public override Result Handle(AddAnalog c) => _remoteRepository
            .BorrowBy(c.RemoteName, r => r.AddAnalog(c.PointName, c.PointCoordinate));
    }
}