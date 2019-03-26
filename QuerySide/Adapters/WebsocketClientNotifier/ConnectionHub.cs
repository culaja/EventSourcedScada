using System.Collections.Generic;
using Common;
using QuerySide.QueryCommon;
using WebSocketSharp;
using static Common.Nothing;

namespace QuerySide.Adapters.WebsocketClientNotifier
{
    public sealed class OpenedSocketsHub
    {
        private readonly List<WebSocket> _sockets = new List<WebSocket>();

        public void Add(WebSocket socket)
        {
            lock (_sockets)
            {
                _sockets.Add(socket);
            }
        }

        public void Remove(WebSocket socket)
        {
            lock (_sockets)
            {
                _sockets.Remove(socket);
            }
        }

        public Nothing NotifyAll(IView v)
        {
            lock (_sockets)
            {
                foreach (var s in _sockets) SendToSocket(s, v);
            }

            return NotAtAll;
        }

        private static void SendToSocket(WebSocket s, IView v) =>
            s.Send(WebsocketMessage.WebsocketMessageFrom(v).Serialize());
    }
}