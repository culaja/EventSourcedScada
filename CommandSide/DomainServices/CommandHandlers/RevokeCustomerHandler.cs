using CommandSidePorts.Repositories;
using Common;
using Common.Messaging;
using Domain.Commands;

namespace DomainServices.CommandHandlers
{
    public sealed class RevokeCustomerHandler : CommandHandler<RevokeCustomer>
    {
        private readonly ICustomerQueueRepository _repository;

        public RevokeCustomerHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(RevokeCustomer c) => _repository
            .BorrowSingle(cq => cq.RevokeCustomer(c.CounterName));

    }
}