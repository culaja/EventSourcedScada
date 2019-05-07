using System;
using System.Collections.Generic;
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

        private readonly HashSet<PointName> _analogPoints = new HashSet<PointName>();

        public Result<Remote> AddAnalog(PointName pointName, PointCoordinate pointCoordinate)
        {
            if (!_analogPoints.Contains(pointName))
            {
                ApplyChange(new AnalogAdded(Id, pointName, pointCoordinate));
                return Result.Ok(this);
            }
            
            return Result.Fail<Remote>($"Analog point with name {pointName} already exists.");
        }

        private void Apply(AnalogAdded e)
        {
            _analogPoints.Add(e.PointName.ToPointName());
        }
    }
}