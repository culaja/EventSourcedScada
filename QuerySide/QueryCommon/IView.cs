using Common;
using Common.Messaging;

namespace QuerySide.QueryCommon
{
    public interface IView
    {
        Nothing Apply(IDomainEvent e);
        
        string SerializeToJson();
    }
}