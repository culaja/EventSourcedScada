using System;
using CommandSide.Domain.Queueing.Configuring;
using Shared.CustomerQueue;

namespace CommandSide.Domain.Queueing
{
    public static class ToDomainObjectsExtensions
    {
        public static CounterId ToCounterId(this Guid id) => new CounterId(id);
        public static OpenTime ToOpenTime(this OpenTimeAdded e) => new OpenTime(e.DayOfWeek, e.BeginTimestamp, e.EndTimestamp);
        public static OpenTime ToOpenTime(this OpenTimeRemoved e) => new OpenTime(e.DayOfWeek, e.BeginTimestamp, e.EndTimestamp);
    }
}