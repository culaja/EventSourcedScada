using System.Collections.Generic;
using CommandSide.Domain.Queueing.Configuring;
using Common;
using Common.Time;
using WebApp.Controllers.CommandsDto;
using static CommandSide.Domain.Queueing.Configuring.CounterDetails;
using static CommandSide.Domain.Queueing.Configuring.OpenTime;
using static CommandSide.Domain.Queueing.Configuring.OpenTimes;
using static CommandSide.Domain.Queueing.CounterId;
using static CommandSide.Domain.Queueing.CounterName;

namespace WebApp.Controllers
{
    public static class ToDomainObjectsExtensions
    {
        public static Configuration ToConfiguration(this SetConfigurationDto setConfigurationDto)
            => Configuration.ConfigurationFrom(
                setConfigurationDto.Counters.ToCountersDetails(),
                setConfigurationDto.OpenTimes.ToOpenTimes());
        
        private static CountersDetails ToCountersDetails(this IList<CounterDetailsDto> countersDetailsDto)
            => new CountersDetails(countersDetailsDto.Map(counterDetail => CounterDetailsFrom(
                CounterIdFrom(counterDetail.Number), CounterNameFrom(counterDetail.Name))));
        
        private static OpenTimes ToOpenTimes(this IList<OpenTimeDto> openTimeDtos)
            => OpenTimesFrom(openTimeDtos.Map(openTime => OpenTimeFrom(openTime.DayOfWeek, new TimeOfDay(openTime.From), new TimeOfDay(openTime.To))));
    }
}