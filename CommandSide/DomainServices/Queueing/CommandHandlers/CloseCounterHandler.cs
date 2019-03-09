using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.Queueing.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.Queueing.CommandHandlers
{
    public sealed class CloseCounterHandler : CommandHandler<CloseCounter>
    {
        private readonly ICustomerQueueRepository _repository;

        public CloseCounterHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(CloseCounter c) => _repository
            .BorrowSingle(cq => cq.CloseCounter(c.CounterId));
    }
}