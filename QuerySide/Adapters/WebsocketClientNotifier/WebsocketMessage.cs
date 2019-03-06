using QuerySide.QueryCommon;
using static Newtonsoft.Json.Formatting;
using static Newtonsoft.Json.JsonConvert;

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

        public string Serialize() => SerializeObject(this, Indented);
    }
}