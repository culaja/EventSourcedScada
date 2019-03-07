using CommandSide.Domain.Queueing.Configuring;
using Common;

namespace CommandSide.Domain.Queueing
{
    public sealed class Counter : Entity<CounterId>
    {
        private readonly CounterName _name;

        public Counter(CounterId id, CounterName name) : base(id)
        {
            _name = name;
        }
        
        public CounterDetails ToCounterDetails() => new CounterDetails(Id, _name);
    }
}