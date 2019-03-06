using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing.Configuring
{
    public sealed class OpenTimes : ValueObject<OpenTimes>
    {
        public IReadOnlyList<OpenTime> Items { get; }

        public OpenTimes(IReadOnlyList<OpenTime> items)
        {
            Items = items;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in Items) yield return item;
        }
    }
}