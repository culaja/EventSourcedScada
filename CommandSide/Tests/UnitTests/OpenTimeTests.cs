using System;
using CommandSide.Domain.Queueing.Configuring;
using Common.Time;
using FluentAssertions;
using Xunit;
using static CommandSide.Domain.Queueing.Configuring.OpenTime;
using static CommandSide.Tests.UnitTests.DomainTestValues;

namespace CommandSide.Tests.UnitTests
{
    public sealed class OpenTimeTests
    {
        [Fact]
        public void should_return_open_time_when_valid_values_provided()
        {
            var openTime = OpenTimeFrom("Monday", "09:00", "18:00");

            openTime.Day.Should().Be(DayOfWeek.Monday);
            openTime.BeginTimestamp.Should().Be(Time_09);
            openTime.EndTimestamp.Should().Be(Time_18);
        }

        [Fact]
        public void when_invalid_beginTimestamp_is_provided_should_throw_UnableToCreateTimeOfDayException()
            => Assert.Throws<TimeOfDayFormatException>(() => OpenTimeFrom("Monday", "invalid", "18:00"));
        
        [Fact]
        public void when_invalid_endTimestamp_is_provided_should_throw_UnableToCreateTimeOfDayException()
            => Assert.Throws<TimeOfDayFormatException>(() => OpenTimeFrom("Monday", "09:00", "invalid"));

        [Fact]
        public void when_invalid_day_is_provided_should_throw_UnableToCreateOpenTimeException()
            => Assert.Throws<UnableToCreateOpenTimeException>(() => OpenTimeFrom("invalid", "09:00", "18:00"));
    }
}