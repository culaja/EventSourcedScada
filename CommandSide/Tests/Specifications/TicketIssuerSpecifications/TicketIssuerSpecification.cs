using System;
using CommandSide.Adapters.InMemory;
using CommandSide.CommandSidePorts.Repositories;
using CommandSide.Domain.TicketIssuing;
using Common.Messaging;
using Shared.TicketIssuer;

namespace CommandSide.Tests.Specifications.TicketIssuerSpecifications
{
    public abstract class TicketIssuerSpecification<T> : Specification<TicketIssuer, TicketIssuerCreated, TicketIssuerEvent, T>
        where T : ICommand
    {
        protected ITicketIssuerRepository TicketIssuerRepository => (ITicketIssuerRepository) AggregateRepository;
        
        protected TicketIssuerSpecification(Guid aggregateRootId) : base(
            new TicketIssuerInMemoryRepository(new DomainEventMessageBusAggregator()), 
            () => new TicketIssuer(aggregateRootId))
        {
        }
    }
}