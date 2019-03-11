using Common.Exceptions;

namespace Common.Time
{
    public sealed class TimeOfDayFormatException : BadRequestException
    {
        public TimeOfDayFormatException(Maybe<string> maybeTime) 
            : base($"Not able to create time of day with provided time string '{maybeTime}'")
        {
        }
    }
}