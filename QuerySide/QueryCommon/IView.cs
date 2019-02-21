using Common.Messaging;

namespace QueryCommon
{
    public interface IView
    {
        void Apply(IDomainEvent e);
        
        string SerializeToJson();
    }
}