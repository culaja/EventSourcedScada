using Common.Messaging;

namespace CommandSide.Domain.Queueing.Commands
{
    public sealed class SetCounterConfiguration : ICommand
    {
        public CounterConfiguration CounterConfiguration { get; }

        public SetCounterConfiguration(CounterConfiguration counterConfiguration)
        {
            CounterConfiguration = counterConfiguration;
        }
    }
}