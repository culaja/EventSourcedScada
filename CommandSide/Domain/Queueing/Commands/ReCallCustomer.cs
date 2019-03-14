using Common.Messaging;

namespace CommandSide.Domain.Queueing.Commands
{
    public sealed class ReCallCustomer : ICommand
    {
        public CounterId CounterId { get; }

        public ReCallCustomer(CounterId counterId)
        {
            CounterId = counterId;
        }
    }
}