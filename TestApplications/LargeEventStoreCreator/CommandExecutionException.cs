using System;

namespace LargeEventStoreCreator
{
    public sealed class CommandExecutionException : Exception
    {
        public CommandExecutionException(string error) : base(error)
        {
        }
    }
}