using Common.Messaging;

namespace CommandSide.Domain.Queueing.Commands
{
    public sealed class CloseCounter : ICommand
    {
        public CounterId CounterId { get; }

        public CloseCounter(CounterId counterId)
        {
            CounterId = counterId;
        }
    }
}