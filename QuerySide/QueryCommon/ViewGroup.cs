using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using static Common.Nothing;

namespace QuerySide.QueryCommon
{
    public abstract class ViewGroup<T> : IViewGroup
        where T : IView, new()
    {
        private readonly Dictionary<Id, IView> _viewById = new Dictionary<Id, IView>();
        
        public IView ViewBy(Id id)
        {
            if (!_viewById.TryGetValue(id, out var view))
            {
                view = new T();
                _viewById.Add(id, view);
            }

            return view;
        }

        public async Task<IView> WaitNewVersionOfViewWithId(Id id)
        {
            var view = ViewBy(id);
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
        
        protected Nothing PassEventToAllViews(IDomainEvent e)
        {
            foreach (var view in _viewById.Values) view.Apply(e);
            return NotAtAll;
        }

        protected Nothing PassEventToViewWithId(Id id, IDomainEvent e)
        {
            ViewBy(id).Apply(e);
            return NotAtAll;
        }
    }
}