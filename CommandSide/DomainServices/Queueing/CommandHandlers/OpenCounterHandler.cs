using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.Queueing.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.Queueing.CommandHandlers
{
    public sealed class OpenCounterHandler : CommandHandler<OpenCounter>
    {
        private readonly ICustomerQueueRepository _repository;

        public OpenCounterHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(OpenCounter c) => _repository
            .BorrowSingle(cq => cq.OpenCounter(c.CounterId));
    }
}