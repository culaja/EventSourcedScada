using System.Collections.Immutable;

namespace QuerySide.Views.CustomerQueueViews
{
    public static class ImmutableCollectionsExtensions
    {
        public static ImmutableDictionary<T, TK>.Builder AddOne<T, TK>(
            this ImmutableDictionary<T, TK>.Builder builder,
            T key,
            TK value)
        {
            builder.Add(key, value);
            return builder;
        }
    }
}