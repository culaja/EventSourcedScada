using System.Threading.Tasks;
using Common.Messaging;
using Domain;
using Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers.CommandsDto;
using static System.Guid;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerQueueController : ControllerBase
    {
        private readonly ILocalMessageBus _localMessageBus;

        public CustomerQueueController(ILocalMessageBus localMessageBus)
        {
            _localMessageBus = localMessageBus;
        }
        
        [HttpPost]
        [Route(nameof(AddCustomerQueue))]
        public Task<IActionResult> AddCustomerQueue() => _localMessageBus
            .HandleAsync(new AddCustomerQueue(NewGuid()))
            .ToActionResultAsync();

        [HttpPost]
        [Route(nameof(AddCounter))]
        public Task<IActionResult> AddCounter([FromBody] AddCounterDto dto) => _localMessageBus
                .HandleAsync(new AddCounter(dto.CounterName.ToCounterName()))
                .ToActionResultAsync();

        [HttpPost]
        [Route(nameof(AddTicket))]
        public Task<IActionResult> AddTicket([FromBody] AddTicketDto dto) => _localMessageBus
            .HandleAsync(new AddTicket(
                NewGuid().ToTicketId(),
                dto.TicketNumber))
            .ToActionResultAsync();

        [HttpPost]
        [Route(nameof(RevokeCustomer))]
        public Task<IActionResult> RevokeCustomer([FromBody] RevokeCustomerDto dto) => _localMessageBus
            .HandleAsync(new RevokeCustomer(dto.CounterName.ToCounterName()))
            .ToActionResultAsync();

        [HttpPost]
        [Route(nameof(TakeNextCustomer))]
        public Task<IActionResult> TakeNextCustomer([FromBody] TakeNextCustomerDto dto) => _localMessageBus
            .HandleAsync(new TakeNextCustomer(dto.CounterName.ToCounterName()))
            .ToActionResultAsync();
    }
}