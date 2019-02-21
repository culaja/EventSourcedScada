using Common.Messaging;

namespace Domain.Commands
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