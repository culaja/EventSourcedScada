using System.Collections.Generic;
using CommandSide.Domain.TicketIssuing;
using FluentAssertions;
using Xunit;
using static CommandSide.Domain.TicketIssuing.OpenTimes;
using static CommandSide.Tests.UnitTests.DomainTestValues;

namespace CommandSide.Tests.UnitTests
{
    public sealed class OpenTimesTests
    {
        [Fact]
        public void when_valid_open_times_provided_should_contain_2_items()
            => OpenTimesFrom(new List<OpenTime> {OpenTimeMonday, OpenTimeTuesday}).Count.Should().Be(2);
        
        [Fact]
        public void when_valid_open_times_for_same_day_provided_should_contain_2_items()
            => OpenTimesFrom(new List<OpenTime> {OpenTimeMonday, OpenTimeMonday2}).Count.Should().Be(2);

        [Fact]
        public void when_overlapping_times_provided_should_throw_OpenTimesAreOverlappingException()
            => Assert.Throws<OpenTimesAreOverlappingException>(() =>
                OpenTimesFrom(new List<OpenTime> {OpenTimeMonday, OpenTimeMondayOverlapping}));
    }
}