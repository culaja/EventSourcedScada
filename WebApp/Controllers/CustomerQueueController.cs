using Common.Messaging;
using Microsoft.AspNetCore.Mvc;
using QuerySide.Views.CustomerQueueViews;
using QuerySide.Views.CustomerQueueViews.Configuring;

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
    }
}