using Common;
using Common.Messaging;
using Microsoft.AspNetCore.Mvc;
using QuerySide.Views;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class VMaxController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly ViewsHolder _viewHolder;

        public VMaxController(
            ICommandBus commandBus,
            ViewsHolder viewHolder)
        {
            _commandBus = commandBus;
            _viewHolder = viewHolder;
        }
    }
}