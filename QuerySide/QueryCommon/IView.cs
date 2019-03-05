using Common.Messaging;

namespace QuerySide.QueryCommon
{
    public interface IView
    {
        void Apply(IDomainEvent e);
        
        string SerializeToJson();
    }
}