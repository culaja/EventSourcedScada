using System;
using System.Collections.Immutable;
using Common;
using Common.Messaging;
using QuerySide.QueryCommon;
using static Common.Nothing;

namespace QuerySide.Views
{
    public sealed class ViewsHolder
    {
        private readonly ImmutableDictionary<Type, View> _views = ImmutableDictionary.CreateBuilder<Type, View>()
            .ToImmutable();

        private readonly ImmutableDictionary<Type, IGroupView> _viewGroups = ImmutableDictionary.CreateBuilder<Type, IGroupView>()
            .ToImmutable();

        public IView View<T>() where T : IView => _views[typeof(T)];

        public IGroupView GroupView<T>() where T : IGroupView => _viewGroups[typeof(T)];

        public Nothing Apply(IDomainEvent e)
        {
            foreach (var v in _views.Values) v.Apply(e);
            foreach (var vg in _viewGroups.Values) vg.Apply(e);
            return NotAtAll;
        }

        public Nothing ForEachView(Func<IView, Nothing> transformer)
        {
            foreach (var v in _views.Values) transformer(v);
            return NotAtAll;
        }
    }
}