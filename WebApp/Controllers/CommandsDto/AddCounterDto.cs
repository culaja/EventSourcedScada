namespace WebApp.Controllers.CommandsDto
{
    public sealed class AddCounterDto
    {
        public string CounterName { get; }

        public AddCounterDto(string counterName)
        {
            CounterName = counterName;
        }
    }
}