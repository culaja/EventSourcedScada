using System;

namespace CommandSide.Domain
{
    public static class ToDomainExtensions
    {
        public static CounterName ToCounterName(this string s) => new CounterName(s);
        
        public static TicketId ToTicketId(this Guid g) => new TicketId(g);
    }
}