using Common;
using Common.Messaging;
using Domain.Commands;
using Ports.Repositories;

namespace DomainServices
{
    public sealed class TakeNextCustomerHandler : CommandHandler<TakeNextCustomer>
    {
        private readonly ICustomerQueueRepository _repository;

        public TakeNextCustomerHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(TakeNextCustomer c) => _repository.BorrowSingle(
            cq => cq.TakeNextCustomer(c.CounterId, c.Timestamp));
    }
}