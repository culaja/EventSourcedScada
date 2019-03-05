using Common.Messaging;
using Newtonsoft.Json;

namespace QuerySide.QueryCommon
{
    public abstract class View : IView
    {
        public void Apply(IDomainEvent e)
        {
            var applyMethodInfo = GetType().GetMethod("Handle", new[] { e.GetType() });

            if (applyMethodInfo != null)
            {
                applyMethodInfo.Invoke(this, new object[] {e});
            }
        }

        public string SerializeToJson() => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}