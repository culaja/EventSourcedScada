using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.RemoteDomain.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.RemoteHandlers.CommandHandlers
{
    public sealed class UpdateAnalogCoordinateHandler : CommandHandler<UpdateAnalogCoordinate>
    {
        private readonly IRemoteRepository _remoteRepository;

        public UpdateAnalogCoordinateHandler(IRemoteRepository remoteRepository)
        {
            _remoteRepository = remoteRepository;
        }

        public override Result Handle(UpdateAnalogCoordinate c) => _remoteRepository
            .BorrowBy(c.RemoteName, r => r.UpdateAnalogCoordinate(c.PointName, c.NewPointCoordinate));
    }
}