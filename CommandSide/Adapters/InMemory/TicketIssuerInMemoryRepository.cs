using System;
using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.TicketIssuing;
using Common;
using Common.Messaging;
using Shared.TicketIssuer;
using static System.Guid;
using static CommandSide.Domain.TicketIssuing.TicketIssuer;

namespace CommandSide.Adapters.InMemory
{
    public sealed class TicketIssuerInMemoryRepository : InMemoryRepository<TicketIssuer, TicketIssuerCreated>, ITicketIssuerRepository
    {
        public TicketIssuerInMemoryRepository(IDomainEventBus domainEventBus) : base(domainEventBus)
        {
        }

        protected override TicketIssuer CreateInternalFrom(TicketIssuerCreated ticketIssuerCreated) =>
            new TicketIssuer(ticketIssuerCreated.AggregateRootId);

        public Result<TicketIssuer> BorrowSingle(Func<TicketIssuer, Result<TicketIssuer>> transformer) =>
            ExecuteTransformerAndPurgeEvents(
                MaybeFirst.Unwrap(
                    ticketIssuer => ticketIssuer,
                    () =>  AddNew(NewTicketIssuerFrom(NewGuid())).Value),
                transformer);
    }
}