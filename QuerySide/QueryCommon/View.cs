using Common.Messaging;

namespace QueryCommon
{
    public abstract class View
    {
        public void Apply(IDomainEvent e)
        {
            var applyMethodInfo = GetType().GetMethod("Handle", new[] { e.GetType() });

            if (applyMethodInfo != null)
            {
                applyMethodInfo.Invoke(this, new object[] {e});
            }
        }
    }
}