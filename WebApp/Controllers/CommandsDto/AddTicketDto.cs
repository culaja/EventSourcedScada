using System;
using Common.Messaging;

namespace WebApp.Controllers.CommandsDto
{
    public sealed class AddTicketDto : ICommand
    {
        public int TicketNumber { get; }

        public AddTicketDto(int ticketNumber)
        {
            TicketNumber = ticketNumber;
        }
    }
}