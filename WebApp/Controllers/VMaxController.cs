using CommandSide.Domain.Queueing;
using CommandSide.Domain.Queueing.Commands;
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

        [HttpPost]
        [Route(nameof(AddCounter))]
        public IActionResult AddCounter(string counterId) => _commandBus
            .Execute(new AddCounter(counterId.ToCounterId()))
            .ToActionResult();

        [HttpGet]
        [Route(nameof(GetCounter))]
        public IActionResult GetCounter() => Ok(_viewHolder.View<CountersAddedView>());
    }
}