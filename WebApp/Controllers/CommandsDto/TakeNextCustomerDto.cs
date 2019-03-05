using Common.Messaging;

namespace WebApp.Controllers.CommandsDto
{
    public sealed class TakeNextCustomerDto : ICommand
    {
        public string CounterName { get; }

        public TakeNextCustomerDto(
            string counterName)
        {
            CounterName = counterName;
        }
    }
}