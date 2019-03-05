using CommandSidePorts.Repositories;
using Common;
using Common.Messaging;
using Domain.Commands;
using static Domain.CustomerQueue;

namespace DomainServices.CommandHandlers
{
    public sealed class AddCustomerQueueHandler : CommandHandler<AddCustomerQueue>
    {
        private readonly ICustomerQueueRepository _repository;

        public AddCustomerQueueHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(AddCustomerQueue c) => _repository.AddNew(
            NewCustomerQueueFrom(c.AggregateRootId));
    }
}