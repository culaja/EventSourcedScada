using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Messaging;
using static Common.Result;

namespace InMemory
{
    public abstract class InMemoryRepository<T, Tk> : IRepository<T, Tk> 
        where T: AggregateRoot
        where Tk: IAggregateRootCreated
    {
        private readonly Dictionary<Guid, T> _cache = new Dictionary<Guid, T>();
        private readonly UniqueIndexing<T> _uniqueIndexes = new UniqueIndexing<T>();

        public InMemoryRepository(ILocalMessageBus localMessageBus)
        {
            LocalMessageBus = localMessageBus;
        }

        public ILocalMessageBus LocalMessageBus { get; }

        protected bool ContainsIndex(object key) => _uniqueIndexes.ContainsIndex(key);

        protected void AddNewIndex(object key, T value) => _uniqueIndexes.AddIndex(key, value);
        
        protected Result<T> MaybeReadIndex(object key) => _uniqueIndexes.GetValueFor(key)
            .Unwrap(
                Ok,
                () => Fail<T>($"Can't find '{typeof(T).Name}' with key '{key}'"));

        protected abstract T CreateInternalFrom(Tk aggregateRootCreated);

        protected virtual bool ContainsKey(T aggregateRoot)
        {
            return false;
        }

        protected virtual void AddedNew(T aggregateRoot)
        {
        }

        public Result<T> CreateFrom(Tk aggregateRootCreated)
        {
            var aggregateRoot = CreateInternalFrom(aggregateRootCreated);
            return AddNew(aggregateRoot);
        }

        public Result<T> AddNew(T aggregateRoot)
        {
            if (_cache.ContainsKey(aggregateRoot.Id) || ContainsKey(aggregateRoot))
            {
                return Fail<T>($"AggregateRoot with id '{aggregateRoot.Id}' already exists in Aggregate '{typeof(T).Name}'.");
            }
            
            _cache.Add(aggregateRoot.Id, aggregateRoot);
            AddedNew(aggregateRoot);
            return Ok(PurgeAllEvents(aggregateRoot));
        }

        public Result<T> BorrowBy(Guid aggregateRootId, Func<T, Result<T>> transformer) => ReadAggregateFromCash(aggregateRootId)
            .OnSuccess(t => ExecuteTransformerAndPurgeEvents(t, transformer));

        public Maybe<T> MaybeFirst => _cache.Values.FirstOrDefault();

        protected Result<T> ExecuteTransformerAndPurgeEvents(T aggregateRoot, Func<T, Result<T>> transformer)
        {
            var result = transformer(aggregateRoot);
            PurgeAllEvents(aggregateRoot);
            return result;
        }

        private Result<T> ReadAggregateFromCash(Guid aggregateRootId)
        {
            if (!_cache.TryGetValue(aggregateRootId, out var aggregateRoot))
            {
                return Fail<T>($"AggregateRoot with id '{aggregateRootId}' doesn't exist in Aggregate '{typeof(T).Name}'.");
            }

            return Ok(aggregateRoot);
        }

        private T PurgeAllEvents(T aggregateRoot)
        {
            LocalMessageBus.DispatchAll(aggregateRoot.DomainEvents);
            aggregateRoot.ClearDomainEvents();
            return aggregateRoot;
        }
    }
}