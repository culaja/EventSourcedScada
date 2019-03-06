using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing.Configuring
{
    public sealed class CountersDetails : ValueObject<CountersDetails>
    {
        public IReadOnlyList<CounterDetails> Items { get; }

        public CountersDetails(IReadOnlyList<CounterDetails> items)
        {
            Items = items;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in Items) yield return item;
        }
    }
}