using System;
using System.Collections.Immutable;
using System.Text;
using Common.Messaging;
using QuerySide.QueryCommon;
using QuerySide.Views.CustomerQueueViews.Configuring;

namespace QuerySide.Views.CustomerQueueViews
{
    public sealed class CustomerQueueViewHolder
    {
        private readonly ImmutableDictionary<Type, View> _views = ImmutableDictionary.CreateBuilder<Type, View>()
            .AddOne(typeof(ConfigurationView), new ConfigurationView())
            .ToImmutable();

        public IView View<T>() where T : IView => _views[typeof(T)];

        public CustomerQueueViewHolder Apply(IDomainEvent e)
        {
            foreach (var v in _views.Values) v.Apply(e);
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
            foreach (var v in _views.Values) transformer(v);
            return this;
        }
    }
}