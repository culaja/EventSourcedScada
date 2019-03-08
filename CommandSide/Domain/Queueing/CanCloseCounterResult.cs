using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class CanCloseCounterResult : ValueObject<CanCloseCounterResult>
    {
        public static readonly CanCloseCounterResult CounterCanBeClosed = new CanCloseCounterResult(nameof(CounterCanBeClosed));
        public static readonly CanCloseCounterResult CounterIsAlreadyClosed = new CanCloseCounterResult(nameof(CounterIsAlreadyClosed));
        public static readonly CanCloseCounterResult CounterDoesntExist = new CanCloseCounterResult(nameof(CounterDoesntExist));
        
        private readonly string _state;

        private CanCloseCounterResult(string state)
        {
            _state = state;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _state;
        }

        public override string ToString() => _state;
    }
}