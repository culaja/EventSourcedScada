using System;
using System.Collections.Generic;
using System.Text;
using Common.Messaging;
using QueryCommon;

namespace CustomerQueueViews
{
    public sealed class ViewHolder
    {
        private readonly IReadOnlyList<View> _views = new View[]
        {
            new CountersView(),
            new TicketsPerCounterView(),
            new TicketQueueView()
        };

        public void Apply(IDomainEvent e)
        {
            foreach (var v in _views) v.Apply(e);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var v in _views) builder.Append(v);
            return builder.ToString();
        }

        public ViewHolder ForEachView(Func<IView, IView> transformer)
        {
            foreach (var v in _views) transformer(v);
            return this;
        }
    }
}