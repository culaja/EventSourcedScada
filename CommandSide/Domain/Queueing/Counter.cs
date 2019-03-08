using CommandSide.Domain.Queueing.Configuring;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class Counter : Entity<CounterId>
    {
        private readonly CounterName _name;
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
            return Nothing.NotAtAll;
        }

        public bool CanOpen() => !_isOpened;
    }
}