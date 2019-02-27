using System;
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
        public IActionResult AddCustomerQueue()
        {
            _localMessageBus
                .Dispatch(new AddCustomerQueue(NewGuid()));
            return new JsonResult("OK");
        }

        [HttpPost]
        [Route(nameof(AddCounter))]
        public IActionResult AddCounter([FromBody] AddCounterDto dto)
        {
            _localMessageBus
                .Dispatch(new AddCounter(
                    dto.CounterName.ToCounterName()));
            return new JsonResult("OK");
        }

        [HttpPost]
        [Route(nameof(AddTicket))]
        public IActionResult AddTicket([FromBody] AddTicketDto dto)
        {
            _localMessageBus
                .Dispatch(new AddTicket(
                    NewGuid().ToTicketId(),
                    dto.TicketNumber));
            return new JsonResult("OK");
        }

        [HttpPost]
        [Route(nameof(RevokeCustomer))]
        public IActionResult RevokeCustomer([FromBody] RevokeCustomerDto dto)
        {
            _localMessageBus
                .Dispatch(new RevokeCustomer(dto.CounterName.ToCounterName()));
            return new JsonResult("OK");
        }

        [HttpPost]
        [Route(nameof(TakeNextCustomer))]
        public IActionResult TakeNextCustomer([FromBody] TakeNextCustomerDto dto)
        {
            _localMessageBus
                .Dispatch(new TakeNextCustomer(
                    dto.CounterName.ToCounterName()));
            return new JsonResult("OK");
        }
    }
}