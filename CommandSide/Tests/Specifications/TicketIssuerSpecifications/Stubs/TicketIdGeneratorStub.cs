using CommandSide.CommandSidePorts;
using CommandSide.Domain;

namespace CommandSide.Tests.Specifications.TicketIssuerSpecifications.Stubs
{
    public sealed class TicketIdGeneratorStub : ITicketIdGenerator
    {
        private readonly TicketId _ticketIdToReturn;

        public TicketIdGeneratorStub(TicketId ticketIdToReturn)
        {
            _ticketIdToReturn = ticketIdToReturn;
        }

        public TicketId GenerateUniqueTicketId() => _ticketIdToReturn;
    }
}