using Common.Exceptions;

namespace Common.Time
{
    public sealed class UnableToCreateTimeOfDayException : BadRequestException
    {
        public UnableToCreateTimeOfDayException(Maybe<string> maybeTime) 
            : base($"Not able to create time of day with provided time '{maybeTime}'")
        {
        }
    }
}