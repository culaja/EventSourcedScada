using System;
using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class CounterId : Id
    {
        private readonly int _id;

        public CounterId(int id)
        {
            _id = id;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _id;
        }

        public override string ToString() => _id.ToString();

        public static implicit operator int(CounterId id) => id._id;
    }
}