using CommandSide.Domain.Queueing;
using FluentAssertions;
using Xunit;
using static CommandSide.Domain.Queueing.CounterName;

namespace CommandSide.Tests.UnitTests
{
    public sealed class CounterNameTests
    {
        [Fact]
        public void should_return_counter_name_when_valid_value_is_provided()
            => CounterNameFrom("Counter 5").Should().NotBeNull();

        [Fact]
        public void when_invalid_value_is_provided_CounterNameCantBeEmptyException_is_thrown()
            => Assert.Throws<CounterNameCantBeEmptyException>(() => CounterNameFrom(null));

        [Fact]
        public void when_empty_string_is_provided_should_return_empty_counter_name_object()
            => CounterNameFrom("").Should().NotBeNull();
    }
}