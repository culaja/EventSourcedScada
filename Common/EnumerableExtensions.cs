using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class EnumerableExtensions
    {
        public static Maybe<T> MaybeFirst<T>(this IEnumerable<T> enumerable) => enumerable.FirstOrDefault();

        public static Maybe<T> MaybeFirst<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) => enumerable.FirstOrDefault(predicate);
    }
}