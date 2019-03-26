using System.Collections.Generic;
using Common;
using Common.Messaging;
using QuerySide.QueryCommon;

namespace QuerySide.Tests.Views
{
    public abstract class ViewSpecification<T> where T : IView, new()
    {
        protected T View { get; } = new T();

        protected ViewSpecification()
        {
            WhenApplied().Map(View.Apply);
        }

        protected abstract IEnumerable<IDomainEvent> WhenApplied();
    }
}