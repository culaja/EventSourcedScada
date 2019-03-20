using CommandSide.Domain.Queueing;
using CommandSide.Domain.Queueing.Commands;
using Common;
using Common.Messaging;
using Shared.TicketIssuer.Events;

namespace CommandSide.DomainServices.TicketIssuingToQueueing
{
    public sealed class EnqueueCustomerWhenTicketIssuedHandler : EventHandler<TicketIssued>
    {
        private readonly ICommandBus _commandBus;

        public EnqueueCustomerWhenTicketIssuedHandler(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public override Result Handle(TicketIssued e) => 
            _commandBus.ScheduleOneWayCommand(new EnqueueCustomer(e.TicketId.ToTicketId()))
            .ToOkResult();
    }
}