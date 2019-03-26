using Common.Exceptions;

namespace CommandSide.Domain.TicketIssuing
{
    public sealed class OpenTimesAreOverlappingException : BadRequestException
    {
        public OpenTimesAreOverlappingException(OpenTime openTime)
            : base($"Open time item '{openTime}' is overlapping with one of the open times in open times collection.")
        {
        }
    }
}