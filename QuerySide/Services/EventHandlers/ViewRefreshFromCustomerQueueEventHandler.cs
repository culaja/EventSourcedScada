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

        public ViewRefreshFromCustomerQueueEventHandler(CountersView countersView)
        {
            _countersView = countersView;
        }
        
        public override Result Handle(CustomerQueueEvent e)
        {
            _countersView.Apply(e);
            Console.WriteLine(_countersView);
            return Ok();
        }
    }
}