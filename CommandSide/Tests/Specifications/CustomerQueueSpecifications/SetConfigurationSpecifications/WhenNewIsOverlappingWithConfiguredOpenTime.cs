using System;
using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static CommandSide.Domain.Queueing.Configuring.Configuration;
using static CommandSide.Tests.Specifications.CustomerQueueConfigurationTestValues;
using static CommandSide.Tests.Specifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.SetConfigurationSpecifications
{
    public sealed class WhenNewIsOverlappingWithConfiguredOpenTime : CustomerQueueSpecification<SetConfiguration>
    {
        public WhenNewIsOverlappingWithConfiguredOpenTime() : base(SingleCustomerQueueId)
        {
        }

        protected override SetConfiguration CommandToExecute =>  new SetConfiguration(EmptyConfiguration.AddOpenTime(Monday10To11));
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            yield return Monday9To12Added;
        }

        public override CommandHandler<SetConfiguration> When() => new SetConfigurationHandler(CustomerQueueRepository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();
    }
}