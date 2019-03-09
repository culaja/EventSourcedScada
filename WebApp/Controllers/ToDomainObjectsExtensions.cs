using System.Collections.Generic;
using System.Linq;
using CommandSide.Domain.Queueing;
using CommandSide.Domain.Queueing.Configuring;
using Common;
using WebApp.Controllers.CommandsDto;

namespace WebApp.Controllers
{
    public static class ToDomainObjectsExtensions
    {
        public static Result<Configuration> ToConfiguration(this SetConfigurationDto setConfigurationDto)
            => setConfigurationDto.Counters.ToCountersDetails()
                .OnSuccess(counterDetails => setConfigurationDto.OpenTimes.ToOpenTimes()
                    .OnSuccess(openTimes => new Configuration(counterDetails, openTimes)));

        private static Result<CountersDetails> ToCountersDetails(this IList<CounterDetailsDto> countersDetailsDto)
        {
            IList<Result<CounterDetails>> countersDetails = new List<Result<CounterDetails>>();
            foreach (var counterDetail in countersDetailsDto)
            {
                countersDetails.Add(CounterDetails.CounterDetailsFrom(
                    CounterId.CounterIdFrom(counterDetail.Number), CounterName.CounterNameFrom(counterDetail.Name)));
            }

            return Result.Combine(countersDetails.ToArray())
                .Map(() => new CountersDetails(countersDetails.Map(cd => cd.Value)));
        }
        
        private static Result<OpenTimes> ToOpenTimes(this IList<OpenTimeDto> openTimeDtos)
        {
            IList<Result<OpenTime>> openTimes = new List<Result<OpenTime>>();
            foreach (var openTime in openTimeDtos)
            {
                openTimes.Add(OpenTime.OpenTimeFrom(openTime.DayOfWeek, openTime.From, openTime.To));
            }

            return Result.Combine(openTimes.ToArray())
                .Map(() => new OpenTimes(openTimes.Map(ot => ot.Value)));
        }
    }
}