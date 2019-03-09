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

        public static Result<CounterDetails> CounterDetailsFrom(Result<CounterId> counterIdResult, Result<CounterName> counterNameResult)
            => Result.Combine(counterIdResult, counterNameResult)
                .Map(() => new CounterDetails(counterIdResult.Value, counterNameResult.Value));
        
        public bool IsTheSameCounterWithDifferentNameAs(CounterDetails counterDetails) =>
            IsTheSameCounterAs(counterDetails) && HasDifferentNameThan(counterDetails);

        public bool IsNotTheSameCounterAs(CounterDetails counterDetails) => Id != counterDetails.Id;
        private bool IsTheSameCounterAs(CounterDetails counterDetails) => Id == counterDetails.Id;
        private bool HasDifferentNameThan(CounterDetails counterDetails) => Name != counterDetails.Name;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
        }
    }
}