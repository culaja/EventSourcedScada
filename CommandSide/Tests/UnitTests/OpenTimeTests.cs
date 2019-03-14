using CommandSide.Domain.TicketIssuing;
using Xunit;
using static System.DayOfWeek;
using static CommandSide.Tests.UnitTests.DomainTestValues;

namespace CommandSide.Tests.UnitTests
{
    public sealed class OpenTimeTests
    {
        [Fact]
        public void when_end_time_is_before_begin_time_BeginTimeNeedsToBeBeforeEndTimeException_is_thrown()
        {
            Assert.Throws<BeginTimeNeedsToBeBeforeEndTimeException>(() =>
                OpenTime.OpenTimeFrom(Monday, Time_17, Time_09));
        }
    }
}