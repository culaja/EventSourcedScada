using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.TicketIssuing.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.TicketIssuing.CommandHandlers
{
    public sealed class ResetTicketIssuerHandler : CommandHandler<ResetTicketIssuer>
    {
        private readonly ITicketIssuerRepository _repository;

        public ResetTicketIssuerHandler(ITicketIssuerRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(ResetTicketIssuer message) => _repository
            .BorrowSingle(ti => ti.Reset());
    }
}