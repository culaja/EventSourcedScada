using Common.Messaging;

namespace CommandSide.Domain.Commands
{
    public sealed class RevokeCustomer : ICommand
    {
        public CounterName CounterName { get; }

        public RevokeCustomer(
            CounterName counterName)
        {
            CounterName = counterName;
        }
    }
}