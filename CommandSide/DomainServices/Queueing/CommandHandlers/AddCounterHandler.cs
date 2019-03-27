using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.Queueing.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.Queueing.CommandHandlers
{
    public sealed class AddCounterHandler : CommandHandler<AddCounter>
    {
        private readonly ICustomerQueueRepository _repository;

        public AddCounterHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(AddCounter c) => _repository
            .BorrowSingle(customerQueue => customerQueue.AddCounter(c.CounterId));
    }
}