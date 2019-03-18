using System.Collections.Generic;

namespace Common
{
    public static class ReadOnlyDictionaryExtensions
    {
        public static Maybe<TK> MaybeGetValue<T, TK>(this IReadOnlyDictionary<T, TK> dictionary, T key) =>
            dictionary.TryGetValue(key, out var value) ? Maybe<TK>.From(value) : Maybe<TK>.None;
    }
}