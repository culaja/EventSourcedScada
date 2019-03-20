using CommandSide.Domain.Queueing;
using CommandSide.Domain.Queueing.Commands;
using Common;
using Common.Messaging;
using Shared.TicketIssuer.Events;

namespace CommandSide.DomainServices.TicketIssuingToQueueing
{
    public sealed class ServeOutOfLineCustomerWhenOutOfLineTicketIssuedHandler : EventHandler<OutOfLineTicketIssued>
    {
        private readonly ICommandBus _commandBus;

        public ServeOutOfLineCustomerWhenOutOfLineTicketIssuedHandler(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public override Result Handle(OutOfLineTicketIssued e) =>
            _commandBus.ScheduleOneWayCommand(new ServeOutOfLineCustomer(
                    e.CounterId.ToCounterId(),
                    e.TicketId.ToTicketId()))
                .ToOkResult();
    }
}