using System;
using Common.Messaging;

namespace Domain.Commands
{
    public sealed class AddTicket : ICommand
    {
        public TicketId TicketId { get; }
        public int TicketNumber { get; }

        public AddTicket(TicketId ticketId, int ticketNumber)
        {
            TicketId = ticketId;
            TicketNumber = ticketNumber;
        }
    }
}