using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing.Configuring
{
    public sealed class CounterDetails : ValueObject<CounterDetails>
    {
        public CounterId Id { get; }
        public CounterName Name { get; }

        public CounterDetails(
            CounterId id,
            CounterName name)
        {
            Id = id;
            Name = name;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
        }
    }
}