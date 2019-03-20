using System;
using System.Collections.Immutable;
using Common;
using Common.Messaging;
using QuerySide.QueryCommon;
using QuerySide.Views.AssigningCustomer;
using QuerySide.Views.Configuring;
using QuerySide.Views.QueueHistory;
using QuerySide.Views.QueueStatus;
using QuerySide.Views.System;
using static Common.Nothing;

namespace QuerySide.Views
{
    public sealed class ViewsHolder
    {
        private readonly ImmutableDictionary<Type, View> _views = ImmutableDictionary.CreateBuilder<Type, View>()
            .AddOne(typeof(ConfigurationView), new ConfigurationView())
            .AddOne(typeof(SystemStatusView), new SystemStatusView())
            .AddOne(typeof(QueueStatusView), new QueueStatusView())
            .AddOne(typeof(QueueHistoryView), new QueueHistoryView())
            .ToImmutable();
        
        private readonly ImmutableDictionary<Type, IGroupView> _viewGroups = ImmutableDictionary.CreateBuilder<Type, IGroupView>()
            .AddOne(typeof(AssignedCustomerGroupView), new AssignedCustomerGroupView())
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