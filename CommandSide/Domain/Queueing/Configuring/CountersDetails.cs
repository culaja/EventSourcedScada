using System.Collections;
using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing.Configuring
{
    public sealed class CountersDetails : ValueObject<CountersDetails>, IReadOnlyList<CounterDetails>
    {
        private readonly IReadOnlyList<CounterDetails> _items;

        public CountersDetails(IReadOnlyList<CounterDetails> items)
        {
            _items = items;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in _items) yield return item;
        }

        public IEnumerator<CounterDetails> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        public int Count => _items.Count;

        public CounterDetails this[int index] => _items[index];
    }
}