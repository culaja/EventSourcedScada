using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.Queueing.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.Queueing.CommandHandlers
{
    public sealed class EnqueueCustomerHandler : CommandHandler<EnqueueCustomer>
    {
        private readonly ICustomerQueueRepository _repository;

        public EnqueueCustomerHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(EnqueueCustomer c) => _repository
            .BorrowSingle(cq => cq.EnqueueCustomer());
    }
}