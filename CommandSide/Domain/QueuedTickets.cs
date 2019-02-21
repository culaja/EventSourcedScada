using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using static Common.Result;

namespace Domain
{
    public sealed class QueuedTickets : ValueObject<QueuedTickets>
    {
        public IReadOnlyList<Ticket> Tickets { get; }

        public QueuedTickets(IReadOnlyList<Ticket> tickets)
        {
            Tickets = tickets;
        }
        
        public static readonly QueuedTickets EmptyQueuedTickets = new QueuedTickets(new List<Ticket>());

        public Result CanAddFrom(TicketId ticketId, int ticketNumber, DateTime ticketPrintingTimestamp) =>
            Validate(ticketId)
                .OnSuccess(() => Validate(ticketNumber));
        
        private Result Validate(TicketId ticketId) => Tickets.ContainsEntityWith(ticketId).OnBoth(
            () => Fail<Ticket>($"{nameof(Ticket)} with Id {ticketId} already exist."),
            Ok);
        
        private Result Validate(int ticketNumber) => Tickets.Select(t => t.Number).Contains(ticketNumber)
            .OnBoth(
                () => Fail<Ticket>($"{nameof(Ticket)} with number {ticketNumber} already exist."),
                Ok);
        
        public QueuedTickets AddFrom(
            TicketId ticketId, 
            int ticketNumber,
            DateTime ticketPrintingTimestamp) => new QueuedTickets(new List<Ticket>(Tickets)
        {
            new Ticket(ticketId, ticketNumber, ticketPrintingTimestamp)
        });

        public Maybe<Ticket> MaybeNextTicket => Tickets.MaybeFirst();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in Tickets) yield return item;
        }

        public QueuedTickets RemoveWithId(TicketId ticketId)
        {
            return new QueuedTickets(new List<Ticket>(Tickets
                .Where(t => t.Id != ticketId)));
        }

        public Ticket GetWithId(TicketId ticketId) => Tickets.First(t => t.Id == ticketId);
    }
}