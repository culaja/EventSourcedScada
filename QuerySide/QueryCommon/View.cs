﻿using Common.Messaging;
using Newtonsoft.Json;
using static Newtonsoft.Json.JsonConvert;

namespace QuerySide.QueryCommon
{
    public abstract class View : IView
    {
        private readonly AsyncManualResetEvent _versionIncrementedEvent = new AsyncManualResetEvent();
        
        public virtual IView Apply(IDomainEvent e)
        {
            var applyMethodInfo = GetType().GetMethod("Handle", new[] { e.GetType() });

            if (applyMethodInfo != null)
            {
                applyMethodInfo.Invoke(this, new object[] {e});
            }
            
            return this;
        }

        public virtual string SerializeToJson() => SerializeObject(this, Formatting.Indented);

        public override string ToString() => SerializeToJson();
    }
}