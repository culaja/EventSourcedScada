using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class CounterId : Id
    {
        private readonly string _counterName;

        public CounterId(string counterName)
        {
            _counterName = counterName;
        }

        public override string ToString() => _counterName;

        public static implicit operator string(CounterId id) => id.ToString();
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _counterName;
        }
    }
}