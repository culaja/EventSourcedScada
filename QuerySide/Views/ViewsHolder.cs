using System;
using System.Collections.Immutable;
using System.Text;
using Common.Messaging;
using QuerySide.QueryCommon;
using QuerySide.Views.Configuring;

namespace QuerySide.Views
{
    public sealed class ViewsHolder
    {
        private readonly ImmutableDictionary<Type, View> _views = ImmutableDictionary.CreateBuilder<Type, View>()
            .AddOne(typeof(ConfigurationView), new ConfigurationView())
            .ToImmutable();

        public IView View<T>() where T : IView => _views[typeof(T)];

        public ViewsHolder Apply(IDomainEvent e)
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

        public ViewsHolder ForEachView(Func<IView, IView> transformer)
        {
            foreach (var v in _views.Values) transformer(v);
            return this;
        }
    }
}