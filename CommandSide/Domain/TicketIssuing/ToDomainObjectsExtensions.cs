using Shared.TicketIssuer.Events;

namespace CommandSide.Domain.TicketIssuing
{
    public static class ToDomainObjectsExtensions
    {
        public static OpenTime ToOpenTime(this OpenTimeAdded e) => new OpenTime(e.DayOfWeek, e.BeginTimestamp, e.EndTimestamp);
        public static OpenTime ToOpenTime(this OpenTimeRemoved e) => new OpenTime(e.DayOfWeek, e.BeginTimestamp, e.EndTimestamp);
    }
}