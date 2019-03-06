using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using static Common.Nothing;

namespace QuerySide.QueryCommon
{
    public abstract class ViewGroup<T, TK>
        where T : Id
        where TK : IView, new()
    {
        private readonly Dictionary<T, TK> _viewById = new Dictionary<T, TK>();

        public async Task<TK> WaitNewVersionOfViewWithId(T id)
        {
            var view = GetViewBy(id);
            await view.WaitNewVersionAsync();
            return view;
        }
        
        public Nothing Apply(IDomainEvent e)
        {
            var applyMethodInfo = GetType().GetMethod("Handle", new[] { e.GetType() });

            if (applyMethodInfo != null)
            {
                applyMethodInfo.Invoke(this, new object[] {e});
            }
            
            return NotAtAll;
        }

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