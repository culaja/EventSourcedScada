using Common.Messaging;

namespace CommandSide.Domain.Queueing.Commands
{
    public sealed class NextCustomer : ICommand
    {
        public CounterId CounterId { get; }

        public NextCustomer(CounterId counterId)
        {
            CounterId = counterId;
        }
    }
}