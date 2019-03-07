using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Newtonsoft.Json;
using static Common.Nothing;

namespace QuerySide.QueryCommon
{
    public abstract class View : IView
    {
        private readonly AsyncManualResetEvent _versionIncrementedEvent = new AsyncManualResetEvent();
        
        public ulong Version { get; private set; }

        public virtual IView Apply(IDomainEvent e)
        {
            var applyMethodInfo = GetType().GetMethod("Handle", new[] { e.GetType() });

            if (applyMethodInfo != null)
            {
                applyMethodInfo.Invoke(this, new object[] {e});
                IncrementVersion();
            }
            
            return this;
        }

        private Nothing IncrementVersion()
        {
            Version++;
            _versionIncrementedEvent.Set();
            return NotAtAll;
        }

        public virtual string SerializeToJson() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        public Task WaitNewVersionAsync()
        {
            _versionIncrementedEvent.Reset();
            return _versionIncrementedEvent.WaitAsync();
        }
    }
}