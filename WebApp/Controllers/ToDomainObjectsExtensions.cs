using System.Collections.Generic;
using CommandSide.Domain;
using Common;
using Common.Time;
using WebApp.Controllers.CommandsDto;
using static CommandSide.Domain.CounterDetails;
using static CommandSide.Domain.OpenTime;
using static CommandSide.Domain.OpenTimes;
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