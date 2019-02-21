using System;
using Common;
using CustomerQueueViews;
using Shared.CustomerQueue;
using static Common.Result;

namespace Services.EventHandlers
{
    public sealed class ViewRefreshFromCustomerQueueEventHandler : Common.Messaging.EventHandler<CustomerQueueEvent>
    {
        private readonly ViewHolder _viewHolder;

        public ViewRefreshFromCustomerQueueEventHandler(
            ViewHolder viewHolder)
        {
            _viewHolder = viewHolder;
        }
        
        public override Result Handle(CustomerQueueEvent e)
        {
            _viewHolder.Apply(e);
            Console.WriteLine(_viewHolder);
            return Ok();
        }
    }
}