using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.TicketIssuing.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.TicketIssuing.CommandHandlers
{
    public sealed class SetOpenTimesHandler : CommandHandler<SetOpenTimes>
    {
        private readonly ITicketIssuerRepository _repository;

        public SetOpenTimesHandler(ITicketIssuerRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(SetOpenTimes c) => _repository
            .BorrowSingle(ti => ti.SetOpenTimes(c.OpenTimes));
    }
}