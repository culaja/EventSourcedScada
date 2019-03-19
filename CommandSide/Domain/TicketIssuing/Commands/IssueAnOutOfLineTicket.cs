using Common.Messaging;

namespace CommandSide.Domain.TicketIssuing.Commands
{
    public sealed class IssueAnOutOfLineTicket : ICommand
    {
        public CounterId CounterId { get; }

        public IssueAnOutOfLineTicket(CounterId counterId)
        {
            CounterId = counterId;
        }
    }
}