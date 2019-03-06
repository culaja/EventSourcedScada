using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Common.Messaging;
using QuerySide.QueryCommon;
using static Common.Nothing;

namespace QuerySide.Views.CustomerQueueViews
{
    public sealed class ViewHolder
    {
        private readonly IReadOnlyList<View> _views = new View[]
        {
        };

        public Nothing Apply(IDomainEvent e)
        {
            foreach (var v in _views) v.Apply(e);
            return NotAtAll;
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