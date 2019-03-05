using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.CommandHandlers
{
    public sealed class AddCounterHandler : CommandHandler<AddCounter>
    {
        private readonly ICustomerQueueRepository _repository;

        public AddCounterHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(AddCounter c) => _repository.BorrowSingle(
            cq => cq.AddCounter(c.CounterName));
    }
}