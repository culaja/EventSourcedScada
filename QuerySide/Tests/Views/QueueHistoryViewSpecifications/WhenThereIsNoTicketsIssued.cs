using System.Collections.Generic;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.QueueHistory;
using Xunit;

namespace QuerySide.Tests.Views.QueueHistoryViewSpecifications
{
    public sealed class WhenThereIsNoTicketsIssued : ViewSpecification<QueueHistoryView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield break;
        }

        [Fact]
        public void history_is_empty() => View.TicketHistory.Should().BeEmpty();
    }
}