using System;

namespace WebApp.Controllers.CommandsDto
{
    public sealed class OpenTimeDto
    {
        public DayOfWeek DayOfWeek { get; }
        public TimeSpan From { get; }
        public TimeSpan To { get; }

        public OpenTimeDto(DayOfWeek dayOfWeek, TimeSpan from, TimeSpan to)
        {
            DayOfWeek = dayOfWeek;
            From = from;
            To = to;
        }
    }
}