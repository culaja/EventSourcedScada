using System.Collections.Generic;
using CommandSide.Domain.RemoteDomain.Commands;
using CommandSide.DomainServices.RemoteHandlers.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.Remote;
using Xunit;
using static CommandSide.Tests.Specifications.RemoteSpecifications.RemoteTestValues;

namespace CommandSide.Tests.Specifications.RemoteSpecifications.AddRemoteSpecifications
{
    public sealed class WhenRemoteDoesntExist : RemoteSpecification<AddRemote>
    {
        protected override AddRemote CommandToExecute => new AddRemote(Remote1Id, Remote1Name);
        
        public override IEnumerable<RemoteEvent> Given()
        {
            yield break;
        }

        public override CommandHandler<AddRemote> When() => new AddRemoteHandler(RemoteRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void produces_remote_created() => ProducedEvents.Should().Contain(Remote1Created);
    }
}