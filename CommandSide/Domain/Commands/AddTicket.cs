using System;
using Common.Messaging;

namespace Domain.Commands
{
    public sealed class AddTicket : ICommand
    {
        public Guid TicketId { get; }
        public int TicketNumber { get; }
        public DateTime TicketPrintingTimestamp { get; }

        public AddTicket(Guid ticketId, int ticketNumber, DateTime ticketPrintingTimestamp)
        {
            TicketId = ticketId;
            TicketNumber = ticketNumber;
            TicketPrintingTimestamp = ticketPrintingTimestamp;
        }
    }
}