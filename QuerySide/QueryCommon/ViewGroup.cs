using System.Collections.Generic;
using Common;
using Common.Messaging;

namespace QuerySide.QueryCommon
{
    public abstract class ViewGroup<T, TK>
        where T : Id
        where TK : IView, new()
    {
        private readonly Dictionary<T, TK> _viewById = new Dictionary<T, TK>();

        protected Nothing PassEventToViewWithId(T id, IDomainEvent e) =>
            GetViewBy(id)
            .Apply(e);

        private TK GetViewBy(T id)
        {
            if (!_viewById.TryGetValue(id, out var view))
            {
                view = new TK();
                _viewById.Add(id, view);
            }

            return view;
        }
    }
}