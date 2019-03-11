using Common;
using Common.Exceptions;

namespace CommandSide.Domain.Queueing.Configuring
{
    public sealed class UnableToCreateOpenTimeException : BadRequestException
    {
        public UnableToCreateOpenTimeException(Maybe<string> maybeDayOfWeek, Maybe<string> maybeBeginTimestamp, Maybe<string> maybeEndTimestamp) 
            : base($"Unable to create open time with provided arguments: DayOfWeek '{maybeDayOfWeek}'," +
                   $" BeginTimestamp '{maybeBeginTimestamp}', EndTimestamp '{maybeEndTimestamp}'")
        {
        }
    }
}