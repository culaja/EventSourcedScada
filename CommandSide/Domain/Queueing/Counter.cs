using CommandSide.Domain.Queueing.Configuring;
using Common;
using static Common.Nothing;

namespace CommandSide.Domain.Queueing
{
    public sealed class Counter : Entity<CounterId>
    {
        private CounterName _name;
        private bool _isOpened = false;

        public Counter(CounterId id, CounterName name) : base(id)
        {
            _name = name;
        }
        
        public CounterDetails ToCounterDetails() => new CounterDetails(Id, _name);

        public bool AreYou(CounterId id) => Id == id;

        public Nothing Open()
        {
            _isOpened = true;
            return NotAtAll;
        }

        public Nothing Close()
        {
            _isOpened = false;
            return NotAtAll;
        }

        public bool CanOpen() => !_isOpened;
        
        public bool CanClose() => !CanOpen();

        public Result CanServeCustomer() => _isOpened.OnBoth(
            Result.Ok,
            () => Result.Fail($"Counter with ID '{Id}' and name '{_name}' is not opened."));

        public Nothing ChangeName(CounterName newCounterName)
        {
            _name = newCounterName;
            return NotAtAll;
        }
    }
}