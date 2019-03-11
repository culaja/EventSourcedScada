using Common.Exceptions;

namespace CommandSide.Domain.Queueing
{
    public sealed class CounterIdCantBeEmptyException : BadRequestException
    {
        public CounterIdCantBeEmptyException() : base("Counter id can't be empty.")
        {
        }
    }
}