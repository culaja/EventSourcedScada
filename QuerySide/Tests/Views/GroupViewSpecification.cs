using System.Collections.Generic;
using Common;
using Common.Messaging;
using QuerySide.QueryCommon;

namespace Tests.Views
{
    public abstract class GroupViewSpecification<T> where T : IGroupView, new()
    {
        protected T GroupView { get; } = new T();
        
        protected GroupViewSpecification()
        {
            WhenApplied().Map(GroupView.Apply);
        }

        protected abstract IEnumerable<IDomainEvent> WhenApplied();
    }
}