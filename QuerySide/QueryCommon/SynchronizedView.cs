using Common.Messaging;

namespace QuerySide.QueryCommon
{
    /// <summary>
    /// Use this view as a base class if there is a need to access a view from REST GET calls.
    /// This will prevent potential race conditions since two threads will access the same data.
    /// </summary>
    public abstract class SynchronizedView : View
    {
        private readonly object _syncObject = new object();

        public override IView Apply(IDomainEvent e)
        {
            lock (_syncObject)
            {
                return base.Apply(e);
            }
        }

        public override string SerializeToJson()
        {
            lock (_syncObject)
            {
                return base.SerializeToJson();
            }
        }
    }
}