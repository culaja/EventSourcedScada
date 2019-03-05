using Newtonsoft.Json;
using QuerySide.QueryCommon;

namespace QuerySide.Adapters.WebsocketClientNotifier
{
    internal sealed class WebsocketMessage
    {
        public string Type { get; }
        
        public string Event { get; }
        
        private WebsocketMessage(IView v)
        {
            Type = v.GetType().Name;
            Event = v.SerializeToJson();
        }
        
        public static WebsocketMessage WebsocketMessageFrom(IView v) => new WebsocketMessage(v);

        public string Serialize() => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}