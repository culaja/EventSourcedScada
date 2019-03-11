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
            _items = new HashSet<CounterDetails>(items).ToList();
        }
        
        public static CountersDetails EmptyCountersDetails => new CountersDetails(new List<CounterDetails>());
        
        public CountersDetails IsolateCountersToAdd(CountersDetails countersDetails) => new CountersDetails(
            _items.Where(countersDetails.DoesntContain).ToList());

        private bool DoesntContain(CounterDetails counterDetails) =>
            _items.All(counterDetails.IsNotTheSameCounterAs);
        
        public IReadOnlyList<CounterId> IsolateCounterIdsToRemove(CountersDetails countersDetails) => 
            countersDetails.Where(DoesntContain).Select(cd => cd.Id).ToList();

        public CountersDetails IsolateCountersDetailsWhereNameDiffers(CountersDetails countersDetails) => new CountersDetails(
            _items.Where(countersDetails.HasTheSameCounterWithDifferentNameAs).ToList());

        private bool HasTheSameCounterWithDifferentNameAs(CounterDetails counterDetails) => 
            _items.Any(counterDetails.IsTheSameCounterWithDifferentNameAs);

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