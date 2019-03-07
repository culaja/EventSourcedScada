using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Common.Messaging;
using QuerySide.QueryCommon;
using QuerySide.Views.CustomerQueueViews.Configuring;
using static Common.Nothing;

namespace QuerySide.Views.CustomerQueueViews
{
    public sealed class CustomerQueueViewHolder
    {
        private readonly IReadOnlyList<View> _views = new View[]
        {
            new ConfigurationView()
        };

        public CustomerQueueViewHolder Apply(IDomainEvent e)
        {
            foreach (var v in _views) v.Apply(e);
            return this;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var v in _views) builder.Append(v);
            return builder.ToString();
        }

        public CustomerQueueViewHolder ForEachView(Func<IView, IView> transformer)
        {
            foreach (var v in _views) transformer(v);
            return this;
        }
    }
}