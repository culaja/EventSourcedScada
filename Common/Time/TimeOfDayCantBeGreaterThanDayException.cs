using System;
using Common.Exceptions;

namespace Common.Time
{
    public sealed class TimeOfDayCantBeGreaterThanDayException : BadRequestException
    {
        public TimeOfDayCantBeGreaterThanDayException(TimeSpan timespan)
            : base($"Time of day can't be greater than one day. (calculated: {timespan})")
        {
        }
    }
}