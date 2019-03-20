using CommandSide.Domain.Queueing.Commands;
using CommandSide.Domain.TicketIssuing.Commands;
using Common;
using Common.Messaging;
using Microsoft.AspNetCore.Mvc;
using QuerySide.Views;
using QuerySide.Views.AssigningCustomer;
using QuerySide.Views.Configuring;
using QuerySide.Views.QueueStatus;
using QuerySide.Views.System;
using WebApp.Controllers.CommandsDto;
using static CommandSide.Domain.CounterId;

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
        
        [HttpGet]
        [Route(nameof(GetServerVersion))]
        public IActionResult GetServerVersion() => Ok(ServerInfo.Version);
        
        [HttpGet]
        [Route(nameof(GetSystemStatus))]
        public IActionResult GetSystemStatus() => Ok(_viewHolder.View<SystemStatusView>());
        
        [HttpGet]
        [Route(nameof(GetQueueStatus))]
        public IActionResult GetQueueStatus() => Ok(_viewHolder.View<QueueStatusView>());

        [HttpGet]
        [Route(nameof(GetConfiguration))]
        public IActionResult GetConfiguration() => Ok(_viewHolder.View<ConfigurationView>());

        [HttpPost]
        [Route(nameof(SetConfiguration))]
        public IActionResult SetConfiguration([FromBody] SetConfigurationDto setConfigurationDto)
        {
            var r1 = _commandBus.Execute(new SetCounterConfiguration(setConfigurationDto.Counters.ToCounterConfiguration())).ToActionResult();
            var r2 = _commandBus.Execute(new SetOpenTimes(setConfigurationDto.OpenTimes.ToOpenTimes())).ToActionResult();
            return r1.CombineWith(r2);
        }

        [HttpPost]
        [Route(nameof(OpenCounter))]
        public IActionResult OpenCounter(int counterId) => _commandBus
            .Execute(new OpenCounter(NewCounterIdFrom(counterId)))
            .ToActionResult();
        
        [HttpPost]
        [Route(nameof(CloseCounter))]
        public IActionResult CloseCounter(int counterId) => _commandBus
            .Execute(new CloseCounter(NewCounterIdFrom(counterId)))
            .ToActionResult();
        
        [HttpPost]
        [Route(nameof(NextCustomer))]
        public IActionResult NextCustomer(int counterId) =>
            _commandBus.Execute(new NextCustomer(NewCounterIdFrom(counterId)))
                .OnSuccess(() => _viewHolder.GroupView<AssignedCustomerGroupView>().SerializeToJson(counterId.ToCounterId()))
                .ToActionResult();
        
        [HttpPost]
        [Route(nameof(RecallCustomer))]
        public IActionResult RecallCustomer(int counterId) =>
            _commandBus.Execute(new ReCallCustomer(NewCounterIdFrom(counterId)))
                .OnSuccess(() => _viewHolder.GroupView<AssignedCustomerGroupView>().SerializeToJson(counterId.ToCounterId()))
                .ToActionResult();
        
        [HttpPost]
        [Route(nameof(ResetOpenTickets))]
        public IActionResult ResetOpenTickets() => _commandBus
            .Execute(new RemoveWaitingCustomers())
            .ToActionResult();

        [HttpPost]
        [Route(nameof(OutOfLineCustomer))]
        public IActionResult OutOfLineCustomer(int counterId) => _commandBus
            .Execute(new IssueAnOutOfLineTicket(NewCounterIdFrom(counterId)))
            .OnSuccess(() => _viewHolder.GroupView<AssignedCustomerGroupView>().SerializeToJson(counterId.ToCounterId()))
            .ToActionResult();
    }
}