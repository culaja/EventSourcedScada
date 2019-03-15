using CommandSide.Domain;

namespace CommandSide.CommandSidePorts
{
    public interface ITicketIdGenerator
    {
        TicketId GenerateUniqueTicketId();
    }
}