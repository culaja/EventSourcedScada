using Common.Messaging;

namespace CommandSide.Domain.Queueing.Commands
{
    public sealed class OpenCounter : ICommand
    {
        public CounterId CounterId { get; }

        public OpenCounter(CounterId counterId)
        {
            CounterId = counterId;
        }
    }
}