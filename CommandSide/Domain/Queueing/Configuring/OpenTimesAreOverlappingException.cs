using Common.Exceptions;

namespace CommandSide.Domain.Queueing.Configuring
{
    public sealed class OpenTimesAreOverlappingException : BadRequestException
    {
        public OpenTimesAreOverlappingException(OpenTime openTime) 
            : base($"Open time item '{openTime.Day} - {openTime.BeginTimestamp.Time}:{openTime.EndTimestamp.Time}'" +
                   $" is overlapping with one of the open times in open times collection.")
        {
        }
    }
}