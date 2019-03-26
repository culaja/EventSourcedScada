using System.Collections.Generic;
using Common;

namespace QuerySide.Views.AssigningCustomer
{
    public sealed class CounterNumber : Id
    {
        private readonly int _number;

        public CounterNumber(int number)
        {
            _number = number;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _number;
        }

        public override string ToString() => _number.ToString();

        public static implicit operator int(CounterNumber id) => id._number;
    }
}