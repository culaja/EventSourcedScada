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
            new TicketsPerCounterServedView()
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
    }
}