using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.RemoteDomain
{
    public sealed class PointCoordinate : ValueObject<PointCoordinate>
    {
        private readonly int _coordinate;

        private PointCoordinate(int coordinate)
        {
            _coordinate = coordinate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield break;
        }

        public static PointCoordinate PointCoordinateFrom(int coordinate)
        {
            return new PointCoordinate(coordinate);
        }

        public override string ToString() => _coordinate.ToString();

        public static implicit operator int(PointCoordinate pointCoordinate) => pointCoordinate._coordinate;
    }
}