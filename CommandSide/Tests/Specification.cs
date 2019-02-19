using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Messaging;

namespace Tests
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
                    ProducedEvents = aggregateRoot.DomainEvents.Select(e => (TK)e).ToList();
                    Result = r;
                    return r;
                });
        }
        
        protected abstract TL CommandToExecute { get; }
        
        protected IReadOnlyList<TK> ProducedEvents { get; private set; } 
        
        protected Result Result { get; private set; } 
        public abstract IEnumerable<TK> Given();

        public abstract CommandHandler<TL> When();
    }
}