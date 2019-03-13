using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.Queueing.Commands;
using Common;
using Common.Messaging;

namespace CommandSide.DomainServices.Queueing.CommandHandlers
{
    public sealed class SetConfigurationHandler : CommandHandler<SetCounterConfiguration>
    {
        private readonly ICustomerQueueRepository _repository;

        public SetConfigurationHandler(ICustomerQueueRepository repository)
        {
            _repository = repository;
        }

        public override Result Handle(SetCounterConfiguration c) => _repository
            .BorrowSingle(cq => cq.SetCounterConfiguration(c.CounterConfiguration));
    }
}