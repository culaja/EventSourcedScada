using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.Queueing.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.Queueing.CommandHandlers
{
    public sealed class RecallCustomerHandler : CommandHandler<ReCallCustomer>
    {
        private readonly ICustomerQueueRepository _repository;

        public RecallCustomerHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(ReCallCustomer c) => _repository
            .BorrowSingle(cq => cq.ReCallCustomerFor(c.CounterId));
    }
}