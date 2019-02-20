using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class EntityExtensions
    {
        public static bool ContainsEntityWith<T>(this IEnumerable<T> list, Guid id) where T : Entity => 
            list.Select(e => e.Id).Contains(id);

        public static Maybe<T> MaybeEntityWith<T>(this IEnumerable<T> list, Guid id) where T : Entity =>
            list.FirstOrDefault(e => e.Id.Equals(id));
    }
}