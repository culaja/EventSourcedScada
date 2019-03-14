using System;
using System.Collections.Generic;
using Common;
using static System.Guid;

namespace CommandSide.Domain
{
    public sealed class TicketId : Id
    {
        private readonly Guid _id;

        private TicketId(Guid id)
        {
            _id = id;
        }
        
        public static TicketId NewTicketId() => new TicketId(NewGuid());
        public static TicketId TicketIdFrom(Guid guid) => new TicketId(guid);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _id;
        }

        public override string ToString() => _id.ToString();

        public static implicit operator Guid(TicketId ticketId) => ticketId._id;
    }
}