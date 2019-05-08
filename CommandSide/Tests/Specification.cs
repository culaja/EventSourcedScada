using System;
using System.Collections.Generic;
using CommandSide.Tests.Specifications;
using Common;
using Common.Messaging;
using Ports.EventStore;

namespace CommandSide.Tests
{
    public abstract class Specification<T, TJ, TK, TL>
        where T : AggregateRoot
        where TJ : IAggregateRootCreated
        where TK : IDomainEvent
        where TL : ICommand
    {
        private ulong _appliedEventVersion;
        
        public IRepository<T, TJ> AggregateRepository { get; }

        protected Specification(
            IRepository<T, TJ> aggregateRepository)
        {
            AggregateRepository = aggregateRepository;

            Given().Map(e => e as IDomainEvent).ApplyAllTo(AggregateRepository);

            When().Handle(CommandToExecute)
                .OnBoth(r =>
                {
                    Result = r;
                    return r;
                });
        }

        protected abstract TL CommandToExecute { get; }

        protected IReadOnlyList<IDomainEvent> ProducedEvents => ((DomainEventMessageBusAggregator) AggregateRepository.DomainEventBus).ProducedEvents;

        protected TK Apply(TK e) => (TK)e.SetVersion(++_appliedEventVersion);

        protected Result Result { get; private set; }
        public abstract IEnumerable<TK> Given();

        public abstract CommandHandler<TL> When();
    }
}