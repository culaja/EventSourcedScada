using Common;
using Common.Messaging;
using Newtonsoft.Json;
using static Common.Nothing;

namespace QuerySide.QueryCommon
{
    public abstract class View : IView
    {
        public Nothing Apply(IDomainEvent e)
        {
            var applyMethodInfo = GetType().GetMethod("Handle", new[] { e.GetType() });

            if (applyMethodInfo != null)
            {
                applyMethodInfo.Invoke(this, new object[] {e});
            }
            
            return NotAtAll;
        }

        public string SerializeToJson() => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}