using Common.Messaging;

namespace WebApp.Controllers.CommandsDto
{
    public sealed class RevokeCustomerDto : ICommand
    {
        public string CounterName { get; }

        public RevokeCustomerDto(
            string counterName)
        {
            CounterName = counterName;
        }
    }
}