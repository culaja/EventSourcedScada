using System;
using System.Collections.Generic;

namespace Shared.CustomerQueue
{
    public sealed class OpenTimeAdded : CustomerQueueEvent
    {
        public DayOfWeek DayOfWeek { get; }
        public DateTime BeginTimestamp { get; }
        public DateTime EndTimestamp { get; }
        public OpenTimeAdded(
            Guid aggregateRootId,
            DayOfWeek dayOfWeek,
            DateTime beginTimestamp,
            DateTime endTimestamp) : base(aggregateRootId)
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