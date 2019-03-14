using System;
using static CommandSide.Domain.TicketId;

namespace CommandSide.Domain.Queueing
{
    public static class ToDomainObjectsExtensions
    {
        public static CounterId ToCounterId(this int id) => new CounterId(id);
        public static CounterName ToCounterName(this string name) => new CounterName(name);
        public static TicketId ToTicketId(this Guid ticketId) => TicketIdFrom(ticketId);
    }
}