using Common.Messaging;

namespace QuerySide.QueryCommon
{
    public interface IView
    {
        IView Apply(IDomainEvent e);
        
        string SerializeToJson();
    }
}