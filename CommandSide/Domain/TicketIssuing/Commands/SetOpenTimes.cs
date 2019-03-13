using CommandSide.Domain.TicketIssuing.Configuring;
using Common.Messaging;

namespace CommandSide.Domain.TicketIssuing.Commands
{
    public sealed class SetOpenTimes : ICommand
    {
        public OpenTimes OpenTimes { get; }

        public SetOpenTimes(OpenTimes openTimes)
        {
            OpenTimes = openTimes;
        }
    }
}