using Common.Messaging;

namespace CommandSide.Domain.Queueing.Commands
{
    public class AddCounter : ICommand
    {
        public CounterId CounterId { get; }

        public AddCounter(CounterId counterId)
        {
            CounterId = counterId;
        }
    }
}