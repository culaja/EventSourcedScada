using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.CommandHandlers
{
    public sealed class TakeNextCustomerHandler : CommandHandler<TakeNextCustomer>
    {
        private readonly ICustomerQueueRepository _repository;

        public TakeNextCustomerHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(TakeNextCustomer c) => _repository.BorrowSingle(
            cq => cq.TakeNextCustomer(c.CounterName));
    }
}