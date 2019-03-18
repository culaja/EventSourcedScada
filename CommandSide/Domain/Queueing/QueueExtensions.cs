using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using static Common.Nothing;

namespace CommandSide.Domain.Queueing
{
    public static class QueueExtensions
    {
        public static Nothing CanEmptyQueue(this Queue<Customer> queue, Func<Nothing> whenQueueNotEmptyCallback) =>
            queue.MaybeFirst().Map(_ => whenQueueNotEmptyCallback()).Unwrap(
                _ => NotAtAll,
                () => NotAtAll);
        
        public static IReadOnlyList<Guid> AllTicketIds(this Queue<Customer> queue) =>
            queue.Select(c => (Guid)(TicketId) c).ToList();
    }
}