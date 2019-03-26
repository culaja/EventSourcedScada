using System;
using Common.Messaging;

namespace Common
{
    public interface IRepository<T, in TK>
        where T : AggregateRoot
        where TK : IAggregateRootCreated
    {
        IDomainEventBus DomainEventBus { get; }

        Result<T> CreateFrom(TK aggregateRootCreated);

        Result<T> AddNew(T aggregateRoot);

        Result<T> BorrowBy(Guid aggregateRootId, Func<T, Result<T>> transformer);

        Maybe<T> MaybeFirst { get; }
    }
}