using Common;
using Common.Messaging;
using Domain.Commands;
using Ports.Repositories;

namespace DomainServices
{
    public sealed class AddCounterHandler : CommandHandler<AddCounter>
    {
        private readonly ICustomerQueueRepository _repository;

        public AddCounterHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(AddCounter c) => _repository.BorrowSingle(
            cq => cq.AddCounter(c.CounterId, c.CounterName));
    }
}