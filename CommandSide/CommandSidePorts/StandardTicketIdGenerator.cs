using CommandSide.Domain;

namespace CommandSide.CommandSidePorts
{
    public sealed class StandardTicketIdGenerator : ITicketIdGenerator
    {
        public TicketId GenerateUniqueTicketId() => TicketId.NewTicketId();
    }
}