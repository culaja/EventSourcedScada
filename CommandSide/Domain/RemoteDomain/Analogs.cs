using System;
using System.Collections.Generic;
using Common;
using Shared.Remote.Events;
using static CommandSide.Domain.RemoteDomain.Analog;

namespace CommandSide.Domain.RemoteDomain
{
    public sealed class Analogs
    {
        private readonly Guid _remoteId;
        private readonly Dictionary<PointName, Analog> _analogPoints = new Dictionary<PointName, Analog>();

        public Analogs(Guid remoteId)
        {
            _remoteId = remoteId;
        }

        public Result<AnalogAdded> GenerateAnalogAddedFor(PointName pointName, PointCoordinate pointCoordinate) =>
            _analogPoints.MaybeGetValue(pointName).Unwrap(
                _ => Result.Fail<AnalogAdded>($"Analog point with name {pointName} already exists in remote."),
                () => new AnalogAdded(_remoteId, pointName, pointCoordinate).ToOkResult());
        
        public void Add(PointName pointName, PointCoordinate pointCoordinate) =>
            _analogPoints.Add(pointName, AnalogFrom(_remoteId, pointName, pointCoordinate));

        public Result<Maybe<AnalogCoordinateUpdated>> GenerateAnalogCoordinateUpdatedWith(PointName pointName,
            PointCoordinate newPointCoordinate) =>
            _analogPoints.MaybeGetValue(pointName).Unwrap(
                analog => analog.GenerateAnalogCoordinateUpdatedWith(newPointCoordinate).ToOkResult(),
                () => Result.Fail<Maybe<AnalogCoordinateUpdated>>($"Analog point with name '{pointName}' doesnt exist in remote."));
        
        public void UpdateCoordinate(PointName pointName, PointCoordinate newPointCoordinate) =>
            _analogPoints[pointName].Update(newPointCoordinate);
    }
}