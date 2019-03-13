namespace WebApp.Controllers.CommandsDto
{
    public sealed class CounterDetailsDto
    {
        public int Number { get; }
        public string Name { get; }

        public CounterDetailsDto(int number, string name)
        {
            Number = number;
            Name = name;
        }
    }
}