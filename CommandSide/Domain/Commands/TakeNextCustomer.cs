using Common.Messaging;

namespace CommandSide.Domain.Commands
{
    public sealed class TakeNextCustomer : ICommand
    {
        public CounterName CounterName { get; }

        public TakeNextCustomer(
            CounterName counterName)
        {
            CounterName = counterName;
        }
    }
}