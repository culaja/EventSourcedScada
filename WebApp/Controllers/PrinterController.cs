using CommandSide.Domain.TicketIssuing.Commands;
using Common.Messaging;
using Microsoft.AspNetCore.Mvc;
using static CommandSide.Domain.TicketIssuing.Commands.TicketNumber;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class PrinterController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        public PrinterController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }
        
        [HttpPost]
        [Route(nameof(IssueATicket))]
        public IActionResult IssueATicket(int ticketNumber) => _commandBus
            .Execute(new IssueATicket(TicketNumberFrom(ticketNumber)))
            .ToActionResult();
    }
}