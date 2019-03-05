using Common.Messaging;

namespace CommandSide.Domain.Commands
{
    public sealed class AddCounter : ICommand
    {
        public CounterName CounterName { get; }

        public AddCounter(CounterName counterName)
        {
            CounterName = counterName;
        }
    }
}