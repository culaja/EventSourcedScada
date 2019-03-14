using Common.Messaging;

namespace CommandSide.Domain.Queueing.Commands
{
    public sealed class EnqueueCustomer : ICommand
    {
        public TicketId TicketId { get; }

        public EnqueueCustomer(TicketId ticketId)
        {
            TicketId = ticketId;
        }
    }
}