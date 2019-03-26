using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class Customer : Entity<TicketId>
    {
        private Customer(TicketId ticketId) : base(ticketId)
        {
        }

        public static Customer NewCustomerFrom(TicketId ticketId) => new Customer(ticketId);

        public static implicit operator TicketId(Customer c) => c.Id;
    }
}