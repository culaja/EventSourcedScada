using System;

namespace Ports.EventStore
{
    public abstract class EventStoreException : Exception
    {
        protected EventStoreException(string message) : base(message)
        {
        }
    }
}