using CommandSide.Domain.Queueing.Configuring;
using Common;
using static Common.Nothing;
using static Common.Result;

namespace CommandSide.Domain.Queueing
{
    public sealed class Counter : Entity<CounterId>
    {
        private CounterName _name;
        private bool _isOpened;
        private Maybe<Customer> _maybeCurrentlyServingCustomer = Maybe<Customer>.None;

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

        public Result<Maybe<Customer>> CanServeCustomer() => _isOpened.OnBoth(
            () => Ok(_maybeCurrentlyServingCustomer),
            () => Fail<Maybe<Customer>>($"Counter with ID '{Id}' and name '{_name}' is not opened."));

        public Nothing ChangeName(CounterName newCounterName)
        {
            _name = newCounterName;
            return NotAtAll;
        }

        public Nothing Assign(Customer customer)
        {
            _maybeCurrentlyServingCustomer = customer;
            return NotAtAll;
        }

        public Nothing UnassignCustomer()
        {
            _maybeCurrentlyServingCustomer = Maybe<Customer>.None;
            return NotAtAll;
        }

        public Result<Customer> CanRecallCustomer() => _maybeCurrentlyServingCustomer
            .Map(Ok)
            .Unwrap(Fail<Customer>($"Not able to recall customer at Counter with ID '{Id}' and name '{_name}' because no customer is assigned."));
    }
}