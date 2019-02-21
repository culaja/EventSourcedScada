using System.Collections.Generic;
using Common;

namespace Domain
{
    public sealed class CounterName : Id
    {
        private readonly string _name;

        public CounterName(string name)
        {
            _name = name;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _name;
        }

        public static implicit operator string(CounterName counterName) => counterName._name;
    }
}