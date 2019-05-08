using CommandSide.Domain.RemoteDomain;
using CommandSide.Domain.RemoteDomain.Commands;
using Common.Messaging;
using Microsoft.AspNetCore.Mvc;
using QuerySide.Views;
using static System.Guid;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class RemoteController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly ViewsHolder _viewHolder;

        public RemoteController(
            ICommandBus commandBus,
            ViewsHolder viewHolder)
        {
            _commandBus = commandBus;
            _viewHolder = viewHolder;
        }

        [Route(nameof(AddRemote))]
        [HttpPut]
        public IActionResult AddRemote(string remoteName) => _commandBus
            .Execute(new AddRemote(NewGuid(), remoteName.ToRemoteName()))
            .ToActionResult();
        
        [Route(nameof(AddAnalog))]
        [HttpPut]
        public IActionResult AddAnalog(
            string remoteName,
            string pointName,
            int pointCoordinate) => _commandBus
            .Execute(new AddAnalog(
                remoteName.ToRemoteName(),
                pointName.ToPointName(),
                pointCoordinate.ToPointCoordinate()))
            .ToActionResult();
        
        [Route(nameof(UpdateAnalogCoordinate))]
        [HttpPut]
        public IActionResult UpdateAnalogCoordinate(
            string remoteName,
            string pointName,
            int newPointCoordinate) => _commandBus
            .Execute(new UpdateAnalogCoordinate(
                remoteName.ToRemoteName(),
                pointName.ToPointName(),
                newPointCoordinate.ToPointCoordinate()))
            .ToActionResult();
    }
}