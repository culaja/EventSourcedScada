using System;
using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class CounterId : Id
    {
        private readonly Guid _id;

        public CounterId(Guid id)
        {
            _id = id;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _id;
        }

        public override string ToString() => _id.ToString();

        public static implicit operator Guid(CounterId id) => id._id;
    }
}