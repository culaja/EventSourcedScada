using System;
using Common;
using static Common.Maybe<Domain.Ticket>;

namespace Domain
{
    public sealed class Counter : Entity
    {
        public string Name { get; }
        
        public Maybe<Ticket> MaybeServingTicket { get; private set; }

        public Counter(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public void SetServingTicket(Ticket ticket) => MaybeServingTicket = ticket;

        public void RemoveServingTicket() => MaybeServingTicket = None;
    }
}