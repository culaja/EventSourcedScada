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
        public void AddCustomerQueue() => _localMessageBus
            .Dispatch(new AddCustomerQueue(NewGuid()));
        
        [HttpPost]
        [Route(nameof(AddCounter))]
        public void AddCounter([FromBody] AddCounterDto dto) => _localMessageBus
            .Dispatch(new AddCounter(
                dto.CounterName.ToCounterName()));

        [HttpPost]
        [Route(nameof(AddTicket))]
        public void AddTicket([FromBody] AddTicketDto dto) => _localMessageBus
            .Dispatch(new AddTicket(
                NewGuid().ToTicketId(),
                dto.TicketNumber,
                dto.TicketPrintingTimestamp));
        
        [HttpPost]
        [Route(nameof(RevokeCustomer))]
        public void RevokeCustomer([FromBody] RevokeCustomerDto dto) => _localMessageBus
            .Dispatch(new RevokeCustomer(dto.CounterName.ToCounterName()));
        
        [HttpPost]
        [Route(nameof(TakeNextCustomer))]
        public void TakeNextCustomer([FromBody] TakeNextCustomerDto dto) => _localMessageBus
            .Dispatch(new TakeNextCustomer(
                dto.CounterName.ToCounterName(),
                DateTime.Now));
    }
}