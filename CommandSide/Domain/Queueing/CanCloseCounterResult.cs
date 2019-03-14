using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class CanCloseCounterResult : ValueObject<CanCloseCounterResult>
    {
        public static readonly CanCloseCounterResult CounterCanBeClosed = new CanCloseCounterResult(nameof(CounterCanBeClosed));
        public static readonly CanCloseCounterResult CounterCantBeClosed = new CanCloseCounterResult(nameof(CounterCantBeClosed));
        public static readonly CanCloseCounterResult CounterIsAlreadyClosed = new CanCloseCounterResult(nameof(CounterIsAlreadyClosed));
        
        public static CanCloseCounterResult CounterCantBeClosedBecauseOfError(string failureReason) => new CanCloseCounterResult(nameof(CounterCantBeClosed), failureReason);
        
        private readonly string _state;
        public string FailureReason { get; }

        private CanCloseCounterResult(string state, string failureReason = "")
        {
            _state = state;
            FailureReason = failureReason;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _state;
        }

        public override string ToString() => _state;
    }
}