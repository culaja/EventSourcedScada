using System.Collections.Generic;
using Common.Messaging;

namespace WebApp.Controllers.CommandsDto
{
    public sealed class SetConfigurationDto : ICommand
    {
        public IList<CounterDetailsDto> Counters { get; }
        public IList<OpenTimeDto> OpenTimes { get; }

        public SetConfigurationDto(IList<CounterDetailsDto> counters, IList<OpenTimeDto> openTimes)
        {
            Counters = counters;
            OpenTimes = openTimes;
        }
    }
}