using System.Threading.Tasks;
using Common;
using Common.Messaging;

namespace QuerySide.QueryCommon
{
    public interface IViewGroup
    {
        Nothing Apply(IDomainEvent e);

        IView ViewBy(Id id);

        Task<IView> WaitNewVersionOfViewWithId(Id id);
    }
}