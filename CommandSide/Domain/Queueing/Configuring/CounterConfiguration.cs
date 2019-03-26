using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace CommandSide.Domain.Queueing.Configuring
{
    public sealed class CounterConfiguration : ValueObject<CounterConfiguration>, IReadOnlyList<CounterDetails>
    {
        private readonly IReadOnlyList<CounterDetails> _items;

        public CounterConfiguration(IReadOnlyList<CounterDetails> items)
        {
            _items = new HashSet<CounterDetails>(items).ToList();
        }

        public static CounterConfiguration EmptyCounterConfiguration => new CounterConfiguration(new List<CounterDetails>());

        public CounterConfiguration IsolateCountersToAdd(CounterConfiguration counterConfiguration) => new CounterConfiguration(
            _items.Where(counterConfiguration.DoesntContain).ToList());

        private bool DoesntContain(CounterDetails counterDetails) =>
            _items.All(counterDetails.IsNotTheSameCounterAs);

        public IReadOnlyList<CounterId> IsolateCounterIdsToRemove(CounterConfiguration counterConfiguration) =>
            counterConfiguration.Where(DoesntContain).Select(cd => cd.Id).ToList();

        public CounterConfiguration IsolateCountersDetailsWhereNameDiffers(CounterConfiguration counterConfiguration) => new CounterConfiguration(
            _items.Where(counterConfiguration.HasTheSameCounterWithDifferentNameAs).ToList());

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