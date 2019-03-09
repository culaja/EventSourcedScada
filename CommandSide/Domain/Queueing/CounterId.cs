using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class CounterId : Id
    {
        private readonly int _id;

        public CounterId(int id)
        {
            _id = id;
        }

        public static Result<CounterId> CounterIdFrom(int? maybeId)
        {
            if (maybeId.HasValue)
            {
                return Result.Ok(new CounterId(maybeId.Value));
            }
            return Result.Fail<CounterId>("Counter id must have value.");
        }
        
        public static CounterId NewCounterIdFrom(int id) => new CounterId(id);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _id;
        }

        public override string ToString() => _id.ToString();

        public static implicit operator int(CounterId id) => id._id;
    }
}