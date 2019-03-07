using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace CommandSide.Domain.Queueing.Configuring
{
    public sealed class OpenTimes : ValueObject<OpenTimes>, IReadOnlyList<OpenTime>
    {
        private readonly IReadOnlyList<OpenTime> _items;

        public OpenTimes(IReadOnlyList<OpenTime> items)
        {
            _items = items;
        }
        
        public static OpenTimes NoOpenTimes => new OpenTimes(new List<OpenTime>());
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in _items) yield return item;
        }

        public IEnumerator<OpenTime> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        public int Count => _items.Count;

        public OpenTime this[int index] => _items[index];

        public OpenTimes IsolateOpenTimesToAdd(OpenTimes currentOpenTimes) => new OpenTimes(
            _items.Where(cd => !currentOpenTimes.Contains(cd)).ToList());
        
        public OpenTimes IsolateOpenTimesToRemove(OpenTimes currentOpenTimes) => new OpenTimes(
            currentOpenTimes.Except(_items).ToList());

        public OpenTimes Add(OpenTime openTime) => new OpenTimes(new List<OpenTime>(_items) {openTime});
        
        public OpenTimes Remove(OpenTime openTime)
        {
            var newList = new List<OpenTime>(_items);
            newList.Remove(openTime);
            return new OpenTimes(newList);
        }
    }
}