using Common.Messaging;

namespace CommandSide.Domain.Queueing.Commands
{
    public sealed class ServeOutOfLineCustomer : ICommand
    {
        public CounterId CounterId { get; }
        public TicketId TicketId { get; }

        public ServeOutOfLineCustomer(
            CounterId counterId,
            TicketId ticketId)
        {
            CounterId = counterId;
            TicketId = ticketId;
        }
    }
}