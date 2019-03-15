using System;
using System.Collections.Immutable;
using Common;
using Common.Messaging;
using QuerySide.QueryCommon;
using QuerySide.Views.AssigningCustomer;
using QuerySide.Views.Configuring;
using static Common.Nothing;

namespace QuerySide.Views
{
    public sealed class ViewsHolder
    {
        private readonly ImmutableDictionary<Type, View> _views = ImmutableDictionary.CreateBuilder<Type, View>()
            .AddOne(typeof(ConfigurationView), new ConfigurationView())
            .ToImmutable();
        
        private readonly ImmutableDictionary<Type, IViewGroup> _viewGroups = ImmutableDictionary.CreateBuilder<Type, IViewGroup>()
            .AddOne(typeof(AssignedCustomerView), new AssignedCustomerViewGroup())
            .ToImmutable();

        public IView View<T>() where T : IView => _views[typeof(T)];
        
        public IView ViewBy<T>(Id id) where T : IView => _viewGroups[typeof(T)].ViewBy(id);

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