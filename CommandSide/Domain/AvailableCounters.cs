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

        public Result<bool> IsCounterAlreadyServingTicket(Guid counterId) => Counters.MaybeEntityWith(counterId)
            .Unwrap(
                m => Ok(m.IsServingATicket),
                () => Fail<bool>($"{nameof(Counter)} with ID '{counterId}' doesn't exist."));

        public AvailableCounters ServeTicket(Guid counterId, Ticket t)
        {
            Counters.MaybeEntityWith(counterId).Value.Serve(t);
            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in Counters) yield return item;
        }
    };
}