using Common.Exceptions;
using Common.Time;

namespace CommandSide.Domain
{
    public sealed class BeginTimeNeedsToBeBeforeEndTimeException : BadRequestException
    {
        public BeginTimeNeedsToBeBeforeEndTimeException(TimeOfDay beginTimeOfDay, TimeOfDay endTimeOfDay)
            : base($"Begin time needs to be before end t ime. (Begin {beginTimeOfDay}, End: {endTimeOfDay})" )
        {
        }
    }
}