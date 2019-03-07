using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        
        public static CountersDetails EmptyCountersDetails => new CountersDetails(new List<CounterDetails>());
        
        public CountersDetails Except(IReadOnlyList<CounterId> counterIds) => new CountersDetails(
            _items.Where(cd => !counterIds.Contains(cd.Id)).ToList());
        
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