using CommandSide.CommandSidePorts;
using CommandSide.CommandSidePorts.Repositories;
using CommandSide.CommandSidePorts.System;
using CommandSide.Domain.TicketIssuing.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.TicketIssuing.CommandHandlers
{
    public sealed class IssueATicketHandler : CommandHandler<IssueATicket>
    {
        private readonly ITicketIssuerRepository _repository;
        private readonly IUtcTimeProvider _utcTimeProvider;
        private readonly ITicketIdGenerator _ticketIdGenerator;

        public IssueATicketHandler(
            ITicketIssuerRepository repository,
            IUtcTimeProvider utcTimeProvider,
            ITicketIdGenerator ticketIdGenerator)
        {
            _repository = repository;
            _utcTimeProvider = utcTimeProvider;
            _ticketIdGenerator = ticketIdGenerator;
        }

        public override Result Handle(IssueATicket c) => _repository
            .BorrowSingle(ti => ti.IssueATicketWith(
                c.TicketNumber,
                () => _utcTimeProvider.CurrentTime,
                () => _ticketIdGenerator.GenerateUniqueTicketId()));
    }
}