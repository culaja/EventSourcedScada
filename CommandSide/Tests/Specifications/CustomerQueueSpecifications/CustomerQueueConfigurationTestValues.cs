using CommandSide.Domain;
using CommandSide.Domain.Queueing;
using CommandSide.Domain.Queueing.Configuring;
using static CommandSide.Domain.TicketId;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications
{
    public static class CustomerQueueConfigurationTestValues
    {   
        public static readonly CounterId Counter1Id = new CounterId(1);
        public static readonly CounterName Counter1Name = new CounterName("Counter1");
        public static readonly CounterName Counter1ChangedName = new CounterName("Counter1ChangedName");
        public static readonly CounterDetails Counter1Details = new CounterDetails(Counter1Id, Counter1Name);
        public static readonly CounterDetails Counter1DetailsWithChangedName = new CounterDetails(Counter1Id, Counter1ChangedName);
        
        public static readonly CounterId Counter2Id = new CounterId(2);
        public static readonly CounterName Counter2Name = new CounterName("Counter2");
        public static readonly CounterName Counter2ChangedName = new CounterName("Counter2ChangedName");
        public static readonly CounterDetails Counter2Details = new CounterDetails(Counter2Id, Counter2Name);
        public static readonly CounterDetails Counter2DetailsWithChangedName = new CounterDetails(Counter2Id, Counter2ChangedName);
        
        public static readonly CounterId Counter3Id = new CounterId(3);
        public static readonly CounterName Counter3Name = new CounterName("Counter3");
        public static readonly CounterName Counter3ChangedName = new CounterName("Counter3ChangedName");
        public static readonly CounterDetails Counter3Details = new CounterDetails(Counter3Id, Counter3Name);
        public static readonly CounterDetails Counter3DetailsWithChangedName = new CounterDetails(Counter3Id, Counter3ChangedName);

        public static readonly TicketId Customer1TicketId = NewTicketId();
        
        public static readonly CounterConfiguration ThreeCounterConfiguration = new CounterConfiguration(new []
        {
            Counter1Details,
            Counter2Details,
            Counter3Details
        });
        
        public static readonly CounterConfiguration ThreeCounterConfigurationWithAllChangedNames = new CounterConfiguration(new []
        {
            Counter1DetailsWithChangedName,
            Counter2DetailsWithChangedName,
            Counter3DetailsWithChangedName
        });
        
        public static readonly CounterConfiguration ThreeCounterConfigurationWithTwoChangedNames = new CounterConfiguration(new []
        {
            Counter1DetailsWithChangedName,
            Counter2Details,
            Counter3DetailsWithChangedName
        });
        
        public static readonly CounterConfiguration TwoCounterConfiguration = new CounterConfiguration(new []
        {
            Counter1Details,
            Counter2Details
        });
    }
}