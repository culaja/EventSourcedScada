using System.Collections.Generic;
using System.Linq;
using CommandSide.Domain.Queueing.Configuring;
using Common;
using static CommandSide.Domain.Queueing.CanCloseCounterResult;
using static CommandSide.Domain.Queueing.CanOpenCounterResult;
using static CommandSide.Domain.Queueing.CanRecallCustomerResult;
using static CommandSide.Domain.Queueing.CanServeNextCustomerResult;
using static Common.Nothing;

namespace CommandSide.Domain.Queueing
{
    public sealed class Counters : ValueObject<Counters>
    {
        private readonly IReadOnlyList<Counter> _collection;

        private Counters(IReadOnlyList<Counter> collection)
        {
            _collection = collection;
        }

        public CounterConfiguration CounterConfiguration => new CounterConfiguration(_collection.Select(c => c.ToCounterDetails()).ToList());

        public static readonly Counters NoCounters = new Counters(new List<Counter>());

        public Counters AddCounterWith(CounterId id, CounterName name) =>
            new Counters(new List<Counter>(_collection) {new Counter(id, name)});

        public Counters Remove(CounterId id) => new Counters(_collection.Where(c => c.Id != id).ToList());

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _collection;
        }

        public CanOpenCounterResult CanOpenCounter(CounterId counterId) => MaybeCounterWith(counterId)
            .Map(c => c.CanOpen() ? CounterCanBeOpened : CounterIsAlreadyOpened)
            .Unwrap(CounterCantBeOpenedBecauseOfError($"Counter with ID '{counterId}' doesn't exist."));

        public Nothing OpenCounterWith(CounterId counterId) => MaybeCounterWith(counterId)
            .Map(c => c.Open())
            .ToNothing();

        public CanCloseCounterResult CanCloseCounter(CounterId counterId) => MaybeCounterWith(counterId)
            .Map(c => c.CanClose() ? CounterCanBeClosed : CounterIsAlreadyClosed)
            .Unwrap(CounterCantBeClosedBecauseOfError($"Counter with ID '{counterId}' doesn't exist."));

        public Nothing CloseCounterWith(CounterId counterId) => MaybeCounterWith(counterId)
            .Map(c => c.Close())
            .ToNothing();

        public Nothing ChangeCounterName(CounterId counterId, CounterName newCounterName) => MaybeCounterWith(counterId)
            .Map(c => c.ChangeName(newCounterName))
            .Unwrap(NotAtAll);

        public CanServeNextCustomerResult CanServeACustomer(CounterId counterId) => MaybeCounterWith(counterId)
            .Map(c => c.CanServeCustomer().OnBoth(
                CounterCanServeNextCustomerAndItIsCurrentlyServingCustomer,
                CounterCantServeCustomerBecauseOfError))
            .Unwrap(CounterCantServeCustomerBecauseOfError($"Counter with ID '{counterId}' doesn't exist."));

        public Nothing AssignCustomerToCounter(CounterId counterId, Customer customer) => MaybeCounterWith(counterId)
            .Map(c => c.Assign(customer))
            .ToNothing();

        public Nothing UnassignCustomerFromCounter(CounterId counterId) => MaybeCounterWith(counterId)
            .Map(c => c.UnassignCustomer())
            .ToNothing();

        private Maybe<Counter> MaybeCounterWith(CounterId counterId) => _collection.MaybeFirst(c => c.AreYou(counterId));

        public CanRecallCustomerResult CanRecallCustomer(CounterId counterId) => MaybeCounterWith(counterId)
            .Map(c => c.CanRecallCustomer().OnBoth(
                CounterCanRecallCustomerFrom,
                CounterCantBeRecalledBecauseOfError))
            .Unwrap(CounterCantBeRecalledBecauseOfError($"Counter with ID '{counterId}' doesn't exist."));
    }
}