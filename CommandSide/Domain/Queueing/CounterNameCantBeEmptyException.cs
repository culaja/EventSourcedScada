using Common.Exceptions;

namespace CommandSide.Domain.Queueing
{
    public sealed class CounterNameCantBeEmptyException : BadRequestException
    {
        public CounterNameCantBeEmptyException() : base("Counter name can't be empty.")
        {
        }
    }
}