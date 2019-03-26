using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class EntityExtensions
    {
        public static bool ContainsEntityWith<T, TK>(this IEnumerable<T> list, TK id)
            where T : Entity<TK>
            where TK : Id =>
            list.Select(e => e.Id).Contains(id);

        public static Maybe<T> MaybeEntityWith<T, TK>(this IEnumerable<T> list, TK id)
            where T : Entity<TK>
            where TK : Id =>
            list.FirstOrDefault(e => e.Id.Equals(id));
    }
}