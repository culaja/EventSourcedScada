using System;

namespace Common
{
    public static class BoolExtensions
    {
        public static T OnBoth<T>(this bool value, Func<T> trueSupplier, Func<T> falseSupplier)
            => value ? trueSupplier() : falseSupplier();
    }
}