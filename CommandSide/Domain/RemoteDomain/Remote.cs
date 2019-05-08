using System;
using Common;
using Shared.Remote.Events;

namespace CommandSide.Domain.RemoteDomain
{
    public sealed class Remote : AggregateRoot
    {
        private readonly RemoteName _remoteName;
        private readonly Analogs _analogPoints;
        
        public Remote(Guid id, RemoteName remoteName) : base(id)
        {
            _remoteName = remoteName;
            _analogPoints = new Analogs(id);
        }

        public static Remote NewRemoteFrom(
            Guid id,
            RemoteName remoteName)
        {
            var remote = new Remote(id, remoteName);
            remote.ApplyChange(new RemoteCreated(remote.Id, remoteName));
            return remote;
        }

        private Remote Apply(RemoteCreated _) => this;

        public Result<Remote> AddAnalog(PointName pointName, PointCoordinate pointCoordinate) =>
            _analogPoints.GenerateAnalogAddedFor(pointName, pointCoordinate)
                .OnSuccess(e => ApplyChange(e))
                .ToTypedResult(this);

        private void Apply(AnalogAdded e) => 
            _analogPoints.Add(e.PointName.ToPointName(), e.PointCoordinate.ToPointCoordinate());

        public Result<Remote> UpdateAnalogCoordinate(PointName pointName, PointCoordinate newPointCoordinate) =>
            _analogPoints.GenerateAnalogCoordinateUpdatedWith(pointName, newPointCoordinate)
                .OnSuccess(maybeEvent => maybeEvent.Map(ApplyChange))
                .ToTypedResult(this);

        private void Apply(AnalogCoordinateUpdated e) =>
            _analogPoints.UpdateCoordinate(e.PointName.ToPointName(), e.NewPointCoordinate.ToPointCoordinate());
    }
}