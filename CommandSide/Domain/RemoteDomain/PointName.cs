using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.RemoteDomain
{
    public sealed class PointName : Id
    {
        private readonly string _pointName;

        private PointName(string pointName)
        {
            _pointName = pointName;
        }

        public static PointName PointNameFrom(Maybe<string> maybePointName)
        {
            return new PointName(maybePointName.Value);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _pointName;
        }

        public override string ToString() => _pointName;

        public static implicit operator string(PointName pointName) => pointName.ToString();
    }
}