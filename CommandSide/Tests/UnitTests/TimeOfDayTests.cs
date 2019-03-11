using Common.Time;
using FluentAssertions;
using Xunit;
using static System.TimeSpan;
using static CommandSide.Tests.UnitTests.DomainTestValues;
using static Common.Time.TimeOfDay;

namespace CommandSide.Tests.UnitTests
{
    public sealed class TimeOfDayTests
    {
        [Theory]
        [InlineData("1.00:01")]
        [InlineData("8")]
        [InlineData("1.21:01")]
        public void invalid_time_of_day_results_in_throwing_TimeOfDayCantBeGreaterThanDayException(string timeSpanAsString)
        {
            Assert.Throws<TimeOfDayCantBeGreaterThanDayException>(() => TimeOfDayFrom(Parse(timeSpanAsString)));
        }

        [Fact]
        public void time_of_day_is_parsed_correctly_for_9_hrs()
        {
            TimeOfDayFrom(Parse("9:00")).Should().Be(Time_09);
        }
        
        [Fact]
        public void time_of_day_is_parsed_correctly_for_24_hrs()
        {
            TimeOfDayFrom(Parse("1.00:00")).Should().Be(Time_24);
        }
    }
}