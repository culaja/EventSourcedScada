using System;
using Common;

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

        public bool IsServingATicket => MaybeServingTicket.HasValue;

        public Counter Serve(Ticket ticket)
        {
            MaybeServingTicket = ticket;
            return this;
        }
    }
}