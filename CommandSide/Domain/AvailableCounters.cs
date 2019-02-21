using System;
using System.Collections.Generic;
using Common;
using static Common.Result;

namespace Domain
{
    public sealed class AvailableCounters : ValueObject<AvailableCounters>
    {
        public IReadOnlyList<Counter> Counters { get; }
        
        public AvailableCounters(IReadOnlyList<Counter> counters)
        {
            Counters = counters;
        }
        
        public static readonly AvailableCounters NoAvailableCounters = new AvailableCounters(new List<Counter>());

        public Result CheckIfCounterIsAvailableWith(CounterName counterName) => Counters.ContainsEntityWith(counterName).OnBoth(
            () => Fail($"{nameof(Counter)} with name '{counterName}' already exist in {nameof(CustomerQueue)}."),
            Ok);
        
        public AvailableCounters AddNewWith(CounterName counterName) => new AvailableCounters(new List<Counter>(Counters)
        {
            new Counter(counterName)
        });

        public Result<Maybe<Ticket>> GetMaybeServingTicket(CounterName counterName) => Counters.MaybeEntityWith(counterName)
            .Unwrap(
                m => Ok(m.MaybeServingTicket),
                () => Fail<Maybe<Ticket>>($"{nameof(Counter)} with name '{counterName}' doesn't exist."));

        public AvailableCounters SetServingTicketFor(CounterName counterName, Ticket t)
        {
            Counters.MaybeEntityWith(counterName).Value.SetServingTicket(t);
            return this;
        }

        public AvailableCounters RemoveServingTicket(CounterName counterName)
        {
            Counters.MaybeEntityWith(counterName).Value.RemoveServingTicket();
            return this;
        }

        public AvailableCounters MapFirstFree(Action<Counter> onFreeCounterCallback)
        {
            Counters.MaybeFirst(c => c.MaybeServingTicket.HasNoValue).Map(
                counter => OnFreeCounterCallback(counter, onFreeCounterCallback));
            return this;
        }

        private AvailableCounters OnFreeCounterCallback(Counter c, Action<Counter> onFreeCounterCallback)
        {
            onFreeCounterCallback(c);
            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in Counters) yield return item;
        }
    };
}