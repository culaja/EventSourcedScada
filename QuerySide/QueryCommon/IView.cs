using System.Threading.Tasks;
using Common;
using Common.Messaging;

namespace QuerySide.QueryCommon
{
    public interface IView
    {
        ulong Version { get; }
        
        IView Apply(IDomainEvent e);
        
        string SerializeToJson();

        Task WaitNewVersionAsync();
    }
}