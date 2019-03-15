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
        private readonly ILocalTimeProvider _localTimeProvider;
        private readonly ITicketIdGenerator _ticketIdGenerator;

        public IssueATicketHandler(
            ITicketIssuerRepository repository,
            ILocalTimeProvider localTimeProvider,
            ITicketIdGenerator ticketIdGenerator)
        {
            _repository = repository;
            _localTimeProvider = localTimeProvider;
            _ticketIdGenerator = ticketIdGenerator;
        }

        public override Result Handle(IssueATicket c) => _repository
            .BorrowSingle(ti => ti.IssueATicketWith(
                _ticketIdGenerator.GenerateUniqueTicketId(),
                c.TicketNumber,
                () => _localTimeProvider.CurrentTime));
    }
}