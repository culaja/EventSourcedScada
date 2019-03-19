using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.Queueing.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.Queueing.CommandHandlers
{
    public sealed class ServeOutOfLineCustomerHandler : CommandHandler<ServeOutOfLineCustomer>
    {
        private readonly ICustomerQueueRepository _repository;

        public ServeOutOfLineCustomerHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(ServeOutOfLineCustomer c) => _repository
            .BorrowSingle(cq => cq.ServeOutOfLineCustomer(c.CounterId, c.TicketId));
    }
}