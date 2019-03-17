using Common;
using Common.Messaging;

namespace QuerySide.QueryCommon
{
    public interface IGroupView
    {
        IGroupView Apply(IDomainEvent e);
        
        string SerializeToJson(Id id);
    }
}