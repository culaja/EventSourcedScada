using System.Threading.Tasks;
using CommandSide.Domain.Queueing.Commands;
using Common;
using Common.Messaging;
using Microsoft.AspNetCore.Mvc;
using QuerySide.Views.CustomerQueueViews;
using QuerySide.Views.CustomerQueueViews.Configuring;
using WebApp.Controllers.CommandsDto;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerQueueController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly CustomerQueueViewHolder _viewHolder;

        public CustomerQueueController(
            ICommandBus commandBus,
            CustomerQueueViewHolder viewHolder)
        {
            _commandBus = commandBus;
            _viewHolder = viewHolder;
        }

        [HttpGet]
        [Route(nameof(GetConfiguration))]
        public IActionResult GetConfiguration() => Ok(_viewHolder.View<ConfigurationView>());

        [HttpPost]
        [Route(nameof(SetConfiguration))]
        public Task<IActionResult> SetConfiguration([FromBody] SetConfigurationDto setConfigurationDto)
            => setConfigurationDto.ToConfiguration()
                .OnSuccess(configuration => _commandBus
                    .ExecuteAsync(new SetConfiguration(configuration)))
                .ToActionResultAsyncFromResult();
    }
}