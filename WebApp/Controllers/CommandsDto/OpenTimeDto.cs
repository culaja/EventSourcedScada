namespace WebApp.Controllers.CommandsDto
{
    public sealed class OpenTimeDto
    {
        public string DayOfWeek { get; }
        public string From { get; }
        public string To { get; }

        public OpenTimeDto(string dayOfWeek, string from, string to)
        {
            DayOfWeek = dayOfWeek;
            From = from;
            To = to;
        }
    }
}