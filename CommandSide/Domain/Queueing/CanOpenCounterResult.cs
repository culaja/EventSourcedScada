using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class CanOpenCounterResult : ValueObject<CanOpenCounterResult>
    {
        public static readonly CanOpenCounterResult CounterCanBeOpened = new CanOpenCounterResult(nameof(CounterCanBeOpened));
        public static readonly CanOpenCounterResult CounterCantBeOpened = new CanOpenCounterResult(nameof(CounterCantBeOpened));
        public static readonly CanOpenCounterResult CounterIsAlreadyOpened = new CanOpenCounterResult(nameof(CounterIsAlreadyOpened));

        public static CanOpenCounterResult CounterCantBeOpenedBecauseOfError(string failureReason) => new CanOpenCounterResult(nameof(CounterCantBeOpened), failureReason);

        private readonly string _state;
        public string FailureReason { get; }

        private CanOpenCounterResult(string state, string failureReason = "")
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