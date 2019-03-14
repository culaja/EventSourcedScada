using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.Queueing.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.Queueing.CommandHandlers
{
    public sealed class NextCustomerHandler : CommandHandler<NextCustomer>
    {
        private readonly ICustomerQueueRepository _repository;

        public NextCustomerHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(NextCustomer c) => _repository
            .BorrowSingle(cq => cq.NextCustomer(c.CounterId));
    }
}