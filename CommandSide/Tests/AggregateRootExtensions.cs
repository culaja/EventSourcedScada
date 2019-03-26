using Common;

namespace CommandSide.Tests
{
    public static class AggregateRootExtensions
    {
        public static T To<T>(this AggregateRoot aggregateRoot) where T : AggregateRoot =>
            (T) aggregateRoot;
    }
}