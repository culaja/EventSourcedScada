using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class CanOpenCounterResult : ValueObject<CanOpenCounterResult>
    {
        public static readonly CanOpenCounterResult CounterCanBeOpened = new CanOpenCounterResult(nameof(CounterCanBeOpened));
        public static readonly CanOpenCounterResult CounterIsAlreadyOpened = new CanOpenCounterResult(nameof(CounterIsAlreadyOpened));
        public static readonly CanOpenCounterResult CounterDoesntExist = new CanOpenCounterResult(nameof(CounterDoesntExist));
        
        private readonly string _state;

        private CanOpenCounterResult(string state)
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