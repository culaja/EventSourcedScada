using System.Threading.Tasks;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.Domain.TicketIssuing.Commands;
using Common;
using Common.Messaging;
using Microsoft.AspNetCore.Mvc;
using QuerySide.Views;
using QuerySide.Views.AssigningCustomer;
using QuerySide.Views.Configuring;
using WebApp.Controllers.CommandsDto;
using static CommandSide.Domain.Queueing.CounterId;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerQueueController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly ViewsHolder _viewHolder;

        public CustomerQueueController(
            ICommandBus commandBus,
            ViewsHolder viewHolder)
        {
            _commandBus = commandBus;
            _viewHolder = viewHolder;
        }

        [HttpGet]
        [Route(nameof(GetConfiguration))]
        public IActionResult GetConfiguration() => Ok(_viewHolder.View<ConfigurationView>());

        [HttpPost]
        [Route(nameof(SetConfiguration))]
        public async Task<IActionResult> SetConfiguration([FromBody] SetConfigurationDto setConfigurationDto)
        {
            var r1 = await _commandBus.ExecuteAsync(new SetCounterConfiguration(setConfigurationDto.Counters.ToCounterConfiguration())).ToActionResultAsync();
            var r2 = await _commandBus.ExecuteAsync(new SetOpenTimes(setConfigurationDto.OpenTimes.ToOpenTimes())).ToActionResultAsync();
            return r1.CombineWith(r2);
        }

        [HttpPost]
        [Route(nameof(OpenCounter))]
        public Task<IActionResult> OpenCounter(int counterId) => _commandBus
            .ExecuteAsync(new OpenCounter(NewCounterIdFrom(counterId)))
            .ToActionResultAsync();
        
        [HttpPost]
        [Route(nameof(CloseCounter))]
        public Task<IActionResult> CloseCounter(int counterId) => _commandBus
            .ExecuteAsync(new CloseCounter(NewCounterIdFrom(counterId)))
            .ToActionResultAsync();
        
        [HttpPost]
        [Route(nameof(NextCustomer))]
        public Task<IActionResult> NextCustomer(int counterId)
        {
            var newViewVersionTask = _viewHolder.ViewBy<AssignedCustomerView>(counterId.ToCounterNumber()).WaitNewVersionAsync();
             return _commandBus.ExecuteAsync(new CloseCounter(NewCounterIdFrom(counterId)))
                 .OnSuccess(() => newViewVersionTask)
                 .ToActionResultAsync();
        }
    }
}