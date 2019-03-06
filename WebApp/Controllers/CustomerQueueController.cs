using Common.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerQueueController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        public CustomerQueueController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }
    }
}