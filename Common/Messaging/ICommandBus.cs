using System.Threading.Tasks;

namespace Common.Messaging
{
    public interface ICommandBus
    {
        Task<Result> ExecuteAsync(ICommand c);

        Nothing ScheduleOneWayCommand(ICommand c);
    }
}