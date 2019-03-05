using System;
using System.Collections.Generic;
using CommandSide.Tests.Specifications;
using Common;
using Common.Messaging;

namespace CommandSide.Tests
{
    public abstract class Specification<T, TJ, TK, TL> 
        where T : AggregateRoot
        where TJ: IAggregateRootCreated
        where TK : IDomainEvent
        where TL : ICommand
    {
        
        public IRepository<T, TJ> AggregateRepository { get; }

        protected Specification(
            IRepository<T, TJ> aggregateRepository,
            Func<T> aggregateRootCreator)
        {
            AggregateRepository = aggregateRepository;
            
            var aggregateRoot = aggregateRootCreator();
            AggregateRepository.AddNew(aggregateRoot);
            foreach (var e in Given()) aggregateRoot.ApplyFrom(e);
            
            When().Handle(CommandToExecute)
                .OnBoth(r =>
                {
                    Result = r;
                    return r;
                });
        }
        
        protected abstract TL CommandToExecute { get; }

        protected IReadOnlyList<IDomainEvent> ProducedEvents => ((DomainEventMessageBusAggregator)AggregateRepository.DomainEventBus).ProducedEvents;
        
        protected Result Result { get; private set; } 
        public abstract IEnumerable<TK> Given();

        public abstract CommandHandler<TL> When();
    }
}