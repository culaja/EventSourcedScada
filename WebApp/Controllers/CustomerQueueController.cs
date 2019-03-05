using System.Threading.Tasks;
using CommandSide.Domain;
using CommandSide.Domain.Commands;
using Common.Messaging;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers.CommandsDto;
using static System.Guid;

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
        
        [HttpPost]
        [Route(nameof(AddCustomerQueue))]
        public Task<IActionResult> AddCustomerQueue() => _commandBus
            .ExecuteAsync(new AddCustomerQueue(NewGuid()))
            .ToActionResultAsync();

        [HttpPost]
        [Route(nameof(AddCounter))]
        public Task<IActionResult> AddCounter([FromBody] AddCounterDto dto) => _commandBus
                .ExecuteAsync(new AddCounter(dto.CounterName.ToCounterName()))
                .ToActionResultAsync();

        [HttpPost]
        [Route(nameof(AddTicket))]
        public Task<IActionResult> AddTicket([FromBody] AddTicketDto dto) => _commandBus
            .ExecuteAsync(new AddTicket(
                NewGuid().ToTicketId(),
                dto.TicketNumber))
            .ToActionResultAsync();

        [HttpPost]
        [Route(nameof(RevokeCustomer))]
        public Task<IActionResult> RevokeCustomer([FromBody] RevokeCustomerDto dto) => _commandBus
            .ExecuteAsync(new RevokeCustomer(dto.CounterName.ToCounterName()))
            .ToActionResultAsync();

        [HttpPost]
        [Route(nameof(TakeNextCustomer))]
        public Task<IActionResult> TakeNextCustomer([FromBody] TakeNextCustomerDto dto) => _commandBus
            .ExecuteAsync(new TakeNextCustomer(dto.CounterName.ToCounterName()))
            .ToActionResultAsync();
    }
}