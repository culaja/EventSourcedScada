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

        public Result CheckIfCounterIsAvailableWith(Guid counterId) => Counters.ContainsEntityWith(counterId).OnBoth(
            () => Fail($"{nameof(Counter)} with Id {counterId} already exist in {nameof(CustomerQueue)}."),
            Ok);
        
        public AvailableCounters AddNewWith(Guid counterId, string counterName) => new AvailableCounters(new List<Counter>(Counters)
        {
            new Counter(counterId, counterName)
        });

        public Result<Maybe<Ticket>> GetMaybeServingTicket(Guid counterId) => Counters.MaybeEntityWith(counterId)
            .Unwrap(
                m => Ok(m.MaybeServingTicket),
                () => Fail<Maybe<Ticket>>($"{nameof(Counter)} with ID '{counterId}' doesn't exist."));

        public AvailableCounters SetServingTicketFor(Guid counterId, Ticket t)
        {
            Counters.MaybeEntityWith(counterId).Value.SetServingTicket(t);
            return this;
        }

        public AvailableCounters RemoveServingTicket(Guid counterId)
        {
            Counters.MaybeEntityWith(counterId).Value.RemoveServingTicket();
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