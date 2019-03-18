using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.Queueing.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.TicketIssuing.CommandHandlers
{
    public sealed class RemoveWaitingCustomersHandler : CommandHandler<RemoveWaitingCustomers>
    {
        private readonly ICustomerQueueRepository _repository;

        public RemoveWaitingCustomersHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(RemoveWaitingCustomers message) => _repository
            .BorrowSingle(cq => cq.RemoveWaitingCustomers());
    }
}