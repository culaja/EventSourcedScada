using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class CounterName : ValueObject<CounterName>
    {
        private readonly string _name;

        public CounterName(string name)
        {
            _name = name;
        }

        public static Result<CounterName> CounterNameFrom(Maybe<string> maybeName)
        {
            if (maybeName.HasValue)
            {
                return Result.Ok(new CounterName(maybeName.Value));
            }
            return Result.Fail<CounterName>("Counter name must have value.");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _name;
        }
        
        public override string ToString() => _name;

        public static implicit operator string(CounterName name) => name.ToString();
    }
}