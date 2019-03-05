using Common.Messaging;

namespace QuerySide.QueryCommon
{
    public interface IHandle<in T> where T : IDomainEvent
    {
        void Handle(T e);
    }
}