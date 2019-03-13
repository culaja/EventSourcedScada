using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using static CommandSide.Domain.Queueing.CanOpenCounterResult;
using static Common.Nothing;

namespace CommandSide.Domain.Queueing
{
    public sealed class Counters : ValueObject<Counters>, IReadOnlyList<Counter>
    {
        private readonly IReadOnlyList<Counter> _collection;

        public Counters(IReadOnlyList<Counter> collection)
        {
            _collection = collection;
        }

        public CounterConfiguration CounterConfiguration => new CounterConfiguration(_collection.Select(c => c.ToCounterDetails()).ToList());

        public static readonly Counters NoCounters = new Counters(new List<Counter>());

        public Counters AddCounterWith(CounterId id, CounterName name) => 
            new Counters(new List<Counter>(_collection) { new Counter(id, name) });

        public Counters Remove(CounterId id) => new Counters(_collection.Where(c => c.Id != id).ToList());
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _collection;
        }

        public IEnumerator<Counter> GetEnumerator() => _collection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _collection.GetEnumerator();

        public int Count => _collection.Count;

        public Counter this[int index] => _collection[index];
        
        public CanOpenCounterResult CanOpenCounter(CounterId counterId) => MaybeCounterWith(counterId)
            .Map(c => c.CanOpen() ? CounterCanBeOpened : CounterIsAlreadyOpened)
            .Unwrap(CounterDoesntExist);

        public Nothing OpenCounterWith(CounterId counterId) => MaybeCounterWith(counterId)
            .Map(c => c.Open())
            .ToNothing();
        
        public CanCloseCounterResult CanCloseCounter(CounterId counterId) => MaybeCounterWith(counterId)
            .Map(c => c.CanClose() ? CanCloseCounterResult.CounterCanBeClosed : CanCloseCounterResult.CounterIsAlreadyClosed)
            .Unwrap(CanCloseCounterResult.CounterDoesntExist);

        public Nothing CloseCounterWith(CounterId counterId) => MaybeCounterWith(counterId)
            .Map(c => c.Close())
            .ToNothing();

        public Nothing ChangeCounterName(CounterId counterId, CounterName newCounterName) => MaybeCounterWith(counterId)
            .Map(c => c.ChangeName(newCounterName))
            .Unwrap(NotAtAll);

        private Maybe<Counter> MaybeCounterWith(CounterId counterId) => _collection.MaybeFirst(c => c.AreYou(counterId));
    }
}