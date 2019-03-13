using System.Collections.Generic;
using CommandSide.Domain;
using CommandSide.Domain.Queueing.Configuring;
using CommandSide.Domain.TicketIssuing;
using Common;
using Common.Time;
using WebApp.Controllers.CommandsDto;
using static CommandSide.Domain.Queueing.Configuring.CounterDetails;
using static CommandSide.Domain.TicketIssuing.OpenTime;
using static CommandSide.Domain.TicketIssuing.OpenTimes;
using static CommandSide.Domain.Queueing.CounterId;
using static CommandSide.Domain.Queueing.CounterName;

namespace WebApp.Controllers
{
    public static class ToDomainObjectsExtensions
    {
        public static CounterConfiguration ToCounterConfiguration(this IList<CounterDetailsDto> countersDetailsDto)
            => new CounterConfiguration(countersDetailsDto.Map(counterDetail => CounterDetailsFrom(
                CounterIdFrom(counterDetail.Number), CounterNameFrom(counterDetail.Name))));
        
        public static OpenTimes ToOpenTimes(this IList<OpenTimeDto> openTimeDtos)
            => OpenTimesFrom(openTimeDtos.Map(openTime => OpenTimeFrom(openTime.DayOfWeek, new TimeOfDay(openTime.From), new TimeOfDay(openTime.To))));
    }
}