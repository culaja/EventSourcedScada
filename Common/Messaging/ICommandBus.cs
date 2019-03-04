using System.Threading.Tasks;

namespace Common.Messaging
{
    public interface ICommandBus
    {
        Task<Result> ExecuteAsync(IMessage message);
    }
}