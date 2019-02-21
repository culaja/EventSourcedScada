using Common.Messaging;
using static Newtonsoft.Json.Formatting;
using static Newtonsoft.Json.JsonConvert;

namespace QueryCommon
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

        public string SerializeToJson() => SerializeObject(this, Indented);
    }
}