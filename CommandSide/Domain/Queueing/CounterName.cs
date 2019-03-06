using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class CounterName : ValueObject<CounterName>
    {
        private readonly string _name;

        private CounterName(string name)
        {
            _name = name;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _name;
        }
        
        public override string ToString() => _name;

        public static implicit operator string(CounterName name) => name.ToString();
    }
}