using System;
using System.Collections.Generic;
using Common.Time;

namespace Shared.TicketIssuer
{
    public sealed class OpenTimeAdded : TicketIssuerEvent
    {
        public DayOfWeek DayOfWeek { get; }
        public TimeOfDay BeginTimestamp { get; }
        public TimeOfDay EndTimestamp { get; }
        public OpenTimeAdded(
            Guid aggregateRootId,
            DayOfWeek dayOfWeek,
            TimeOfDay beginTimestamp,
            TimeOfDay endTimestamp) : base(aggregateRootId)
        {
            DayOfWeek = dayOfWeek;
            BeginTimestamp = beginTimestamp;
            EndTimestamp = endTimestamp;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return DayOfWeek;
            yield return BeginTimestamp;
            yield return EndTimestamp;
        }
    }
}