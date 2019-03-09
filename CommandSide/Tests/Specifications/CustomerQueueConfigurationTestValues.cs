using CommandSide.Domain.Queueing;
using CommandSide.Domain.Queueing.Configuring;
using Common.Time;
using static System.DayOfWeek;
using static Common.Time.TimeOfDay;

namespace CommandSide.Tests.Specifications
{
    public static class CustomerQueueConfigurationTestValues
    {
        #region Open times
        public static readonly TimeOfDay Hour9 = TimeOfDayFromHour(9);
        public static readonly TimeOfDay Hour10 = TimeOfDayFromHour(10);
        public static readonly TimeOfDay Hour11 = TimeOfDayFromHour(11);
        public static readonly TimeOfDay Hour12 = TimeOfDayFromHour(12);
        public static readonly TimeOfDay Hour14 = TimeOfDayFromHour(14);
        public static readonly TimeOfDay Hour16 = TimeOfDayFromHour(16);
        
        public static readonly OpenTime Monday9To12 = new OpenTime(Monday, Hour9, Hour12);
        public static readonly OpenTime Monday10To11 = new OpenTime(Monday, Hour10, Hour11);
        public static readonly OpenTime Monday14To16 = new OpenTime(Monday, Hour14, Hour16);
        
        public static readonly OpenTime Tuesday9To12 = new OpenTime(Tuesday, Hour9, Hour12);
        
        public static readonly OpenTimes MondayOpenTimes = new OpenTimes(new []
        {
            Monday9To12,
            Monday14To16
        });
        
        public static readonly OpenTimes TuesdayOpenTimes = new OpenTimes(new []
        {
            Tuesday9To12
        });
        
        public static readonly OpenTimes AllOpenTimes = new OpenTimes(new []
        {
            Monday9To12,
            Monday14To16,
            Tuesday9To12
        });
        
        #endregion
        
        #region Counters
        
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
        
        public static readonly CountersDetails ThreeCountersDetails = new CountersDetails(new []
        {
            Counter1Details,
            Counter2Details,
            Counter3Details
        });
        
        public static readonly CountersDetails ThreeCountersDetailsWithAllChangedNames = new CountersDetails(new []
        {
            Counter1DetailsWithChangedName,
            Counter2DetailsWithChangedName,
            Counter3DetailsWithChangedName
        });
        
        public static readonly CountersDetails ThreeCountersDetailsWithTwoChangedNames = new CountersDetails(new []
        {
            Counter1DetailsWithChangedName,
            Counter2Details,
            Counter3DetailsWithChangedName
        });
        
        public static readonly CountersDetails TwoCountersDetails = new CountersDetails(new []
        {
            Counter1Details,
            Counter2Details
        });
        
        #endregion
        
        public static readonly Configuration FullConfiguration = new Configuration(
            ThreeCountersDetails,
            AllOpenTimes);
        
        public static readonly Configuration FullConfigurationWithAllChangedCounterNames = new Configuration(
            ThreeCountersDetailsWithAllChangedNames,
            AllOpenTimes);
        
        public static readonly Configuration FullConfigurationWithTwoChangedCounterNames = new Configuration(
            ThreeCountersDetailsWithTwoChangedNames,
            AllOpenTimes);
        
        public static readonly Configuration ConfigurationWithMondayOpenTimesAndFirstTwoCounters = new Configuration(
            TwoCountersDetails,
            MondayOpenTimes);
    }
}