using System;
using System.Collections.Generic;
using Common;

namespace CommandSide.Domain
{
    public sealed class TicketId : Id
    {
        private readonly Guid _id;

        public TicketId(Guid id)
        {
            _id = id;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _id;
        }

        public static implicit operator Guid(TicketId ticketId) => ticketId._id;
    }
}