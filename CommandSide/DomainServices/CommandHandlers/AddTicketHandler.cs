using CommandSidePorts.Repositories;
using Common;
using Common.Messaging;
using Domain.Commands;
namespace DomainServices.CommandHandlers
{
    public sealed class AddTicketHandler : CommandHandler<AddTicket>
    {
        private readonly ICustomerQueueRepository _repository;

        public AddTicketHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(AddTicket c) => _repository.BorrowSingle(
            cq => cq.AddTicket(c.TicketId, c.TicketNumber));
    }
}