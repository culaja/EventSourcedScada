using System;
using Common;
using CustomerQueueViews;
using Shared.CustomerQueue;
using static Common.Result;

namespace Services.EventHandlers
{
    public sealed class ViewRefreshFromCustomerQueueEventHandler : Common.Messaging.EventHandler<CustomerQueueEvent>
    {
        private readonly CountersView _countersView;
        private readonly TicketsPerCounterServedView _ticketsPerCounterServedView;

        public ViewRefreshFromCustomerQueueEventHandler(
            CountersView countersView,
            TicketsPerCounterServedView ticketsPerCounterServedView)
        {
            _countersView = countersView;
            _ticketsPerCounterServedView = ticketsPerCounterServedView;
        }
        
        public override Result Handle(CustomerQueueEvent e)
        {
            _countersView.Apply(e);
            _ticketsPerCounterServedView.Apply(e);
            Console.WriteLine(_countersView);
            Console.WriteLine(_ticketsPerCounterServedView);
            return Ok();
        }
    }
}