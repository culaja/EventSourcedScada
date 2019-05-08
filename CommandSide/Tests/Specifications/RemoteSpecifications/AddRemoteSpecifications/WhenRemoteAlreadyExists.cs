using System.Collections.Generic;
using CommandSide.Domain.RemoteDomain.Commands;
using CommandSide.DomainServices.RemoteHandlers.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.Remote;
using Shared.Remote.Events;
using Xunit;
using static CommandSide.Tests.AssertionsHelpers;
using static CommandSide.Tests.Specifications.RemoteSpecifications.RemoteTestValues;

namespace CommandSide.Tests.Specifications.RemoteSpecifications.AddRemoteSpecifications
{
    public sealed class WhenRemoteAlreadyExists : RemoteSpecification<AddRemote>
    {
        protected override AddRemote CommandToExecute => new AddRemote(Remote1Id, Remote1Name);
        
        public override IEnumerable<RemoteEvent> Given()
        {
            yield return Apply(Remote1Created);
        }

        public override CommandHandler<AddRemote> When() => new AddRemoteHandler(RemoteRepository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void doesnt_produce_remote_created() =>
            ProducedEvents.Should().NotContain(EventOf<RemoteCreated>());
    }
}