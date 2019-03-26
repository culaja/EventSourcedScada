using Common;
using Common.Messaging;
using Newtonsoft.Json;

namespace QuerySide.QueryCommon
{
    public abstract class GroupView<T> : IGroupView
    {
        public virtual IGroupView Apply(IDomainEvent e)
        {
            var applyMethodInfo = GetType().GetMethod("Handle", new[] {e.GetType()});

            if (applyMethodInfo != null)
            {
                applyMethodInfo.Invoke(this, new object[] {e});
            }

            return this;
        }

        public virtual string SerializeToJson(Id id) => JsonConvert.SerializeObject(GenerateViewFor(id), Formatting.Indented);

        public abstract T GenerateViewFor(Id id);
    }
}