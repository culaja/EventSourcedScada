using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.TicketIssuing.Commands
{
    public sealed class TicketNumber : ValueObject<TicketNumber>
    {
        private readonly int _number;

        private TicketNumber(int number)
        {
            _number = number;
        }
        
        public static readonly TicketNumber FirstTicketNumber = new TicketNumber(1);

        public static TicketNumber TicketNumberFrom(int number) => new TicketNumber(number);
        
        public TicketNumber Next => new TicketNumber(_number + 1);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _number;
        }

        public override string ToString() => _number.ToString();

        public static implicit operator int(TicketNumber ticketNumber) => ticketNumber._number;
    }
}