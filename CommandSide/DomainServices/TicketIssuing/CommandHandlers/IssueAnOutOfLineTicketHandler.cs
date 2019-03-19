using CommandSide.CommandSidePorts;
using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.TicketIssuing.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.TicketIssuing.CommandHandlers
{
    public sealed class IssueAnOutOfLineTicketHandler : CommandHandler<IssueAnOutOfLineTicket>
    {
        private readonly ITicketIssuerRepository _repository;
        private readonly ITicketIdGenerator _ticketIdGenerator;

        public IssueAnOutOfLineTicketHandler(
            ITicketIssuerRepository repository,
            ITicketIdGenerator ticketIdGenerator)
        {
            _repository = repository;
            _ticketIdGenerator = ticketIdGenerator;
        }

        public override Result Handle(IssueAnOutOfLineTicket c) => _repository
            .BorrowSingle(ti => ti.IssueAnOutOfLineTicket(
                c.CounterId,
                _ticketIdGenerator.GenerateUniqueTicketId()));
    }
}