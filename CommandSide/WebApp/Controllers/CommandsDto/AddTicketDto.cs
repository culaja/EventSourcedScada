using System;
using Common.Messaging;

namespace WebApp.Controllers.CommandsDto
{
    public sealed class AddTicketDto : ICommand
    {
        public int TicketNumber { get; }
        public DateTime TicketPrintingTimestamp { get; }

        public AddTicketDto(int ticketNumber, DateTime ticketPrintingTimestamp)
        {
            TicketNumber = ticketNumber;
            TicketPrintingTimestamp = ticketPrintingTimestamp;
        }
    }
}