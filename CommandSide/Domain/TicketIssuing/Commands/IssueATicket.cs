using Common.Messaging;

namespace CommandSide.Domain.TicketIssuing.Commands
{
    public sealed class IssueATicket : ICommand
    {
        public TicketNumber TicketNumber { get; }

        public IssueATicket(TicketNumber ticketNumber)
        {
            TicketNumber = ticketNumber;
        }
    }
}