using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain;
using CommandSide.Domain.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.CommandHandlers
{
    public sealed class AddCustomerQueueHandler : CommandHandler<AddCustomerQueue>
    {
        private readonly ICustomerQueueRepository _repository;

        public AddCustomerQueueHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(AddCustomerQueue c) => _repository.AddNew(
            CustomerQueue.NewCustomerQueueFrom(c.AggregateRootId));
    }
}