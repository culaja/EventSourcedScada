using CommandSide.Domain.Queueing;
using FluentAssertions;
using Xunit;
using static CommandSide.Domain.Queueing.CounterId;

namespace CommandSide.Tests.UnitTests
{
    public sealed class CounterIdTests
    {
        [Fact]
        public void should_return_counter_id_from_valid_value()
            => CounterIdFrom(2).Should().NotBeNull();

        [Fact]
        public void when_no_value_is_provided_CounterIdCantBeEmptyException_is_thrown()
            => Assert.Throws<CounterIdCantBeEmptyException>(() => CounterIdFrom(null));
    }
}