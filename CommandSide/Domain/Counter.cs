using Common;

namespace CommandSide.Domain
{
    public sealed class Counter : Entity<CounterName>
    {
        public Maybe<Ticket> MaybeServingTicket { get; private set; }

        public Counter(CounterName name) : base(name)
        {
        }

        public void SetServingTicket(Ticket ticket) => MaybeServingTicket = ticket;

        public void RemoveServingTicket() => MaybeServingTicket = Maybe<Ticket>.None;
    }
}