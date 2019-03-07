using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CommandSide.Domain.Queueing.Configuring;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class Counters : ValueObject<Counters>, IReadOnlyList<Counter>
    {
        private readonly IReadOnlyList<Counter> _collection;

        public Counters(IReadOnlyList<Counter> collection)
        {
            _collection = collection;
        }

        public CountersDetails CountersDetails => new CountersDetails(_collection.Select(c => c.ToCounterDetails()).ToList());

        public static readonly Counters NoCounters = new Counters(new List<Counter>());

        public Counters AddCounterWith(CounterId id, CounterName name) => 
            new Counters(new List<Counter>(_collection) { new Counter(id, name) });

        public Counters Remove(CounterId id) => new Counters(_collection.Where(c => c.Id == id).ToList());
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _collection;
        }

        public IEnumerator<Counter> GetEnumerator() => _collection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _collection.GetEnumerator();

        public int Count => _collection.Count;

        public Counter this[int index] => _collection[index];
    }
}